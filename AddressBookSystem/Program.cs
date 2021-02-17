using System;
using System.Collections.Generic;
using System.Linq;

namespace AddressBookSystem
{
    class AddressBook
    {
        Dictionary<String, String> Contact = new Dictionary<string, string>();
        static readonly string[] ContactFieldType = { "First Name", "Last Name", "Address", "City", "State", "Zip", "Phone number", "Email" };
        /// <summary>
        /// Initializes a new instance of the <see cref="AddressBook"/> class.
        /// </summary>
        public AddressBook()
        {
            foreach (string field in ContactFieldType)
            {
                Contact.Add(field,"");
            }
        }
        /// <summary>
        /// Adds the contact.
        /// </summary>
        /// <param name="ContactFields">The contact fields.</param>
        private void AddContact(string[] ContactFields)
        {
            if (ContactFieldType.Length.Equals(ContactFields.Length))
            {
                for (int i = 0; i < ContactFieldType.Length; i++)
                {
                    Contact[ContactFieldType[i]] =  ContactFields[i];
                }
            }           
        }
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        static void Main()
        {
            Console.WriteLine("Welcome to Address Book Program");
        }
    }
}
