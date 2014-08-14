using System;

namespace NetTesting.Assertions
{
    public static partial class Assert
    {
        public static void IsNull(Guid test, string message = "Guid was not default")
        {
            if (test != default(Guid)) BlowUp(message);
        }


        public static void IsNotNull(Guid test, string message = "Guid was default")
        {
            if (test == default(Guid)) BlowUp(message);
        }
    }
}
