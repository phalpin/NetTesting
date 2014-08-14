namespace NetTesting.Assertions
{
    public static partial class Assert
    {
        public static void IsTrue(bool val, string message = "Expected true, got false")
        {
            if (!val) BlowUp(message);
        }

        public static void IsFalse(bool val, string message = "Expected false, got true")
        {
            if (val) BlowUp(message);
        }
    }
}
