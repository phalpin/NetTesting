using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NetTesting
{
    /// <summary>
    /// Base Functionality for the Runner.
    /// </summary>
    public abstract class RunnerBase
    {

        #region Singleton Members
        protected static RunnerBase _instance;
        protected Assembly _targetAssembly;
        #endregion

        #region Logging Methodology
        public static Func<string, bool> LogMethod { get; set; }
        public static Func<string, bool> LogSuccessMethod { get; set; }
        public static Func<string, bool> LogErrorMethod { get; set; }

        protected static void DoLog(string msg, params string[] args)
        {
            if(LogMethod != null)
            {
                LogMethod(string.Format(msg, args));
            }
        }

        protected static void DoLogError(string msg, params string[] args)
        {
            if(LogErrorMethod == null)
            {
                DoLog(msg, args);
            }
            else
            {
                LogErrorMethod(string.Format(msg, args));
            }
        }

        protected static void DoLogSuccess(string msg, params string[] args)
        {
            if(LogSuccessMethod == null)
            {
                DoLog(msg, args);
            }
            else
            {
                LogSuccessMethod(string.Format(msg, args));
            }
        }
        #endregion

        #region Common Initialization
        protected virtual void InternalInit(Assembly targetAssembly = null)
        {
            if(targetAssembly == null)
            {
                _targetAssembly = Assembly.GetEntryAssembly();
            }
        }
        #endregion

        public RunnerBase(Assembly targetAssembly = null)
        {
            InternalInit(targetAssembly);
        }

        public virtual void Run()
        {
            
        }

        protected virtual void RunSuites()
        {
        }

        protected virtual void RunTests(Type t)
        {
            DisplayTestHeader(t);
        }


        #region Display Logic
        protected virtual void DisplayTestHeader(Type t)
        {
            TestFixtureAttribute att = (TestFixtureAttribute)(Attribute.GetCustomAttribute(t, typeof(TestFixtureAttribute)));
            if (!string.IsNullOrEmpty(att.Description))
            {
                DoLog("[{0}:{1}]", t.Name, att.Description);
            }
            else
            {
                DoLog("[{0}]", t.Name);
            }
        }

        protected virtual void DisplayTestStatus(Type t, MethodInfo test)
        {
            DoLogSuccess("{0}", test.Name);
        }

        protected virtual void DisplayTestStatus(Assertions.AssertionException aex)
        {
            DoLogError("[{0}]{1}{2}", aex.Method, ":", aex.Message);
        }

        protected virtual void DisplayTestStatus(Exception ex)
        {
            DoLogError("Unhandled Exception: {0}", ex.Message);
        }
        #endregion

        protected bool IsTextFixture(Type t)
        {
            Attribute attTest = Attribute.GetCustomAttribute(t, typeof(TestFixtureAttribute));
            return attTest != null;
        }

        protected bool IsTest(MethodInfo mi)
        {
            Attribute testAttTest = Attribute.GetCustomAttribute(mi, typeof(TestAttribute), false);
            return testAttTest != null;
        }

        /// <summary>
        /// Runs a test. Returns whether or not the test was successful.
        /// </summary>
        /// <returns></returns>
        public bool RunTest()
        {
            return true;
        }


    }
}
