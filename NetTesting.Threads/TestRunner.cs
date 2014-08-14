using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Collections.Concurrent;
using NetTesting.Assertions;

namespace NetTesting
{
    public class TestRunner : RunnerBase
    {
        private static ConcurrentDictionary<Type, ConcurrentBag<MethodInfo>> _types;

        #region Initialization
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="targetAssembly"></param>
        protected TestRunner(Assembly targetAssembly = null)
        {
            InternalInit(targetAssembly);
        }

        /// <summary>
        /// Test Bootstrapper.
        /// </summary>
        /// <param name="targetAssembly"></param>
        protected override void InternalInit(Assembly targetAssembly = null)
        {

            base.InternalInit(targetAssembly);

            _types = new ConcurrentDictionary<Type, ConcurrentBag<MethodInfo>>();
            Parallel.ForEach<Type>(_targetAssembly.GetTypes(), (type) =>
            {
                try
                {
                    if (IsTextFixture(type))
                    {
                        _types.AddOrUpdate(type, new ConcurrentBag<MethodInfo>(), (k, v) =>
                        {
                            return new ConcurrentBag<MethodInfo>();
                        });

                        foreach (MethodInfo i in type.GetMethods())
                        {
                            if (IsTest(i))
                            {
                                _types[type].Add(i);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    DoLogError("Failed to check attribute for type: {0}, error was: {1}", type.Name, ex.Message);
                }
            });

        }
        #endregion

        #region Public Accessors
        /// <summary>
        /// Initialization Logic.
        /// </summary>
        /// <param name="targetAssembly"></param>
        public static void Initialize(Assembly targetAssembly = null)
        {
            if (_instance == null)
            {
                if (targetAssembly == null)
                {
                    targetAssembly = Assembly.GetCallingAssembly();
                }

                _instance = new TestRunner(targetAssembly);
            }
        }

        /// <summary>
        /// Runs All tests found.
        /// </summary>
        public static void RunTests()
        {
            if(_instance != null)
            {
                _instance = new TestRunner();
            }

            _instance.Run();

        }
        #endregion

        #region Running Methods
        /// <summary>
        /// Runs the test plan.
        /// </summary>
        public override void Run()
        {
            RunSuites();
        }

        /// <summary>
        /// Runs each of the suites you've initialized in this runner.
        /// </summary>
        protected override void RunSuites()
        {
            base.RunSuites();

            foreach (Type t in _types.Keys)
            {
                RunTests(t);
            }

        }

        /// <summary>
        /// Runs a set of tests for a given type.
        /// </summary>
        /// <param name="t"></param>
        protected override void RunTests(Type t)
        {
            base.RunTests(t);

            foreach (MethodInfo test in _types[t])
            {
                try
                {
                    object instance = Activator.CreateInstance(t);
                    object[] paramsToRun = { };
                    test.Invoke(instance, paramsToRun);
                    DisplayTestStatus(t, test);
                }
                catch (TargetInvocationException tiex)
                {
                    //Test-Related Failure
                    if (tiex.InnerException is AssertionException)
                    {
                        AssertionException aex = tiex.InnerException as AssertionException;
                        DisplayTestStatus(aex);
                    }

                    //Something else happened.
                    else
                    {
                        DisplayTestStatus(tiex.InnerException);
                    }
                }
            }


        }
        #endregion
    }
}
