using System;
using System.Collections.Generic;

namespace AddressBookSystem
{
    class AddressBook
    {
        Dictionary<String, String> ContactField = new Dictionary<string, string>();
        private void AddContact()
        {
            Console.WriteLine("First Name:");
            ContactField.Add("First Name", Console.ReadLine());

            Console.WriteLine("Last Name:");
            ContactField.Add("Last Name", Console.ReadLine());

            Console.WriteLine("Address:");
            ContactField.Add("Address", Console.ReadLine());

            Console.WriteLine("City:");
            ContactField.Add("City", Console.ReadLine());

            Console.WriteLine("State:");
            ContactField.Add("State", Console.ReadLine());

            Console.WriteLine("Zip:");
            ContactField.Add("Zip", Console.ReadLine());

            Console.WriteLine("Phone number:");
            ContactField.Add("Phone number", Console.ReadLine());

            Console.WriteLine("Email:");
            ContactField.Add("Email", Console.ReadLine());
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Address Book Program");
            AddressBook addressBook = new AddressBook();
            addressBook.AddContact();
        }


    }
}
