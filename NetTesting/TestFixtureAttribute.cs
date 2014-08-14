using System;

namespace NetTesting
{
    public class TestFixtureAttribute : Attribute
    {
        public string Description { get; set; }
        public TestFixtureAttribute() { }
    }
}
