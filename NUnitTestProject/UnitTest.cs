using NUnit.Framework;
using AddressBookSystemNameSpace;
namespace NUnitTestProject
{
    public class Tests
    {
        AddressBookSystem AddressBookManager;

       [SetUp]
        public void Setup()
        {
            AddressBookManager = new AddressBookSystem();
        }

        [Test]
        public void Test1()
        {
            AddressBookManager.AddContact();
           // Assert.Pass();
        }
    }
}