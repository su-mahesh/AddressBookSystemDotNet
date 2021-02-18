using AddressBookSystemNameSpace;
using NUnit.Framework;

namespace NUnitTestProject
{
    public class Tests
    {
        AddressBookSystem AddressBook;
        [SetUp]
        public void Setup()
        {
            AddressBook = new AddressBookSystem();
        }

        [Test]
        public void GivenContact_WhenAdded_ShouldReturnSame()
        { 
            string[] contact = {"Mahesh", "Kangude", "shivajinagar", "pune", "Mah", "111 222","91 4443333333", "mah@dd.com" };
            AddressBook.AddContact(contact);
            string FullName = contact[0] + " " + contact[1];
            string[] result = AddressBook.GetAllContactField(FullName);          
            Assert.AreEqual(contact, result);
        }
        [Test]
        public void GivenAddedContact_WhenEdited_ShouldReturnSame()
        {
            string[] contact = { "Mahesh", "Kangude", "shivajinagar", "pune", "Mah", "111 222", "91 4443333333", "mah@dd.com" };
            AddressBook.AddContact(contact);
            string FullName = contact[0] + " " + contact[1];
            string FirstName = "Mah";
            AddressBook.EditContactDetails(FullName, 0, FirstName);
            string result = AddressBook.GetContactField(FirstName+" "+contact[1], 0);
            Assert.AreEqual(FirstName, result);
        }

        [Test]
        public void GivenAddedContact_WhenDeleted_ShouldReturnFalse()
        {
            string[] contact = { "Mahesh", "Kangude", "shivajinagar", "pune", "Mah", "111 222", "91 4443333333", "mah@dd.com" };
            AddressBook.AddContact(contact);
            string FullName = contact[0] + " " + contact[1];
            AddressBook.DeleteContact(FullName);
            bool result = AddressBook.IsContactPresent(FullName);
            Assert.IsFalse(result);
        }
        [Test]
        public void GivenAddressBook_WhenCreated_ShouldReturnTrue()
        {
            string AddressbookName = "new";
            AddressBook.CreateAddressBook(AddressbookName);
            bool result = AddressBook.CheckAddressBookExist(AddressbookName);
            Assert.IsTrue(result);
        }
    }
}