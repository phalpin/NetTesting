using System;

namespace NetTesting.Assertions
{
    public static partial class Assert
    {
        private static void BlowUp(string msg)
        {
            throw new AssertionException(msg, 1);
        }

        public static void WorksFor<T>(T target, Func<T, bool> test, string message = "Didn't work!")
        {
            if (!test(target))
            {
                BlowUp(message);
            }
        }
    }
}
