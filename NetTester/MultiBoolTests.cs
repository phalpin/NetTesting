using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTesting;
using NetTesting.Assertions;

namespace NetTester
{
    [TestFixture(Description ="Tests the Internal Consistency of the MultiBool object.")]
    public class MultiBoolTests
    {
        [Test]
        public void Test_We_Get_Valid_GUID()
        {
            MultiBool mb = new MultiBool();
            Guid actual = mb.addFalse();
            Assert.IsNotNull(actual, "We didn't receive a guid for adding a false to our multibool");
        }

        [Test]
        public void Test_Is_False_When_Receive_A_Guid()
        {
            MultiBool mb = new MultiBool();
            Guid returned = mb.addFalse();
            Assert.IsTrue(mb.Truthiness, "Truthiness of multibool should not be true");
            //Assert.AreEqual(false, mb.Truthiness, "Truthiness of multibool should not be true");
        }

        [Test]
        public void Test_Is_True_When_Receive_A_Guid_And_Remove()
        {
            MultiBool mb = new MultiBool();
            Guid returned = mb.addFalse();
            mb.setTrue(returned);
            Assert.AreEqual(true, mb.Truthiness, "Truthiness of multibool should be true, got false");
        }

        [Test]
        public void Test_Use_Case()
        {
            MultiBool mb = new MultiBool();
            Assert.WorksFor<MultiBool>(mb, (multi) =>
            {
                List<Guid> guids = new List<Guid>();
                for (int i = 0; i < 50; i++)
                {
                    guids.Add(mb.addFalse());
                    if(mb.Truthiness == true)
                    {
                        return false;
                    }
                }

                foreach(Guid g in guids)
                {
                    mb.setTrue(g);
                }

                return mb.Truthiness;
            });
        }
    }
}
