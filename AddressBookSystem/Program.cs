using System;
using System.Collections.Generic;

namespace AddressBookSystem
{
    class AddressBookSystem
    {
        Dictionary<String, String> Contact; 
        Dictionary<String, Dictionary<String, String>> AddressBook = new Dictionary<string, Dictionary<String, String>>();
        private void AddContact()
        {
            Contact = new Dictionary<string, string>();

            Console.WriteLine("First Name:");
            Contact.Add("First Name", Console.ReadLine());

            Console.WriteLine("Last Name:");
            Contact.Add("Last Name", Console.ReadLine());

            Console.WriteLine("Address:");
            Contact.Add("Address", Console.ReadLine());

            Console.WriteLine("City:");
            Contact.Add("City", Console.ReadLine());

            Console.WriteLine("State:");
            Contact.Add("State", Console.ReadLine());

            Console.WriteLine("Zip:");
            Contact.Add("Zip", Console.ReadLine());

            Console.WriteLine("Phone number:");
            Contact.Add("Phone number", Console.ReadLine());

            Console.WriteLine("Email:");
            Contact.Add("Email", Console.ReadLine());
            Contact.TryGetValue("First Name", out String Name);
            AddressBook.Add(Name, Contact);
            Console.WriteLine("contact added\n");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Address Book Program");
            AddressBookSystem addressBook = new AddressBookSystem();

            while (true)
            {
                Console.WriteLine("***** menu *****");
                Console.WriteLine("1. add contact ");
                try
                {
                    switch (Convert.ToInt32(Console.ReadLine()))
                    {
                        case 1:
                            addressBook.AddContact();
                            break;
                    }
                }
                catch (Exception) {
                    Console.WriteLine("wrong input");
                }                              
            }            
        }
    }
}
