using System;
using System.Diagnostics;

namespace NetTesting.Assertions
{
    public class AssertionException : Exception
    {
        public string Class { get; private set; }
        public string Method { get; private set; }
        public string FullMessage
        {
            get
            {
                return string.Format("[{0}][{1}] Test Failed: {2}", Class, Method, Message);
            }
        }

        public AssertionException(string msg) : base(msg)
        {
            StackFrame sf = new StackFrame(2);
            Method = sf.GetMethod().Name;
            Class = sf.GetMethod().DeclaringType.Name;
        }

        public AssertionException(string msg, int stackFrameOffset) : base(msg)
        {
            StackFrame sf = new StackFrame(2 + stackFrameOffset);
            Method = sf.GetMethod().Name;
            Class = sf.GetMethod().DeclaringType.Name;
        }
        
        public override string ToString()
        {
            return string.Format("[{0}][{1}] Test Failed:{2}", Class, Method, Message);
        }

    }
}
