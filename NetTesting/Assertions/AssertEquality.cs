namespace NetTesting.Assertions
{
    public static partial class Assert
    {
        public static void AreEqual(bool expected, bool actual, string message = "Bools were unequal")
        {
            if (expected != actual) BlowUp(message);
        }
    }
}
