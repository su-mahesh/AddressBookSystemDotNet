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
            Console.WriteLine("add contact");
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
            Contact.TryGetValue("First Name", out String FirstName);
            Contact.TryGetValue("Last Name", out String LastName);
            AddressBook.Add(FirstName+ " " +LastName, Contact);
            Console.WriteLine("contact added\n");
        }
        private void ViewContact()
        {
            Console.WriteLine("Enter full name:");
            String ContactName = Console.ReadLine();
            if (AddressBook.ContainsKey(ContactName))
            {
                Contact = new Dictionary<string, string>();
                AddressBook.TryGetValue(ContactName, out Contact);
                Console.WriteLine("First Name: " + Contact["First Name"]);

                Console.WriteLine("Last Name:" + Contact["Last Name"]);

                Console.WriteLine("Address:" + Contact["Address"]);

                Console.WriteLine("City:" + Contact["City"]);

                Console.WriteLine("State:" + Contact["State"]);

                Console.WriteLine("Zip:" + Contact["Zip"]);

                Console.WriteLine("Phone number:" + Contact["Phone number"]);

                Console.WriteLine("Email:" + Contact["Email"]);
            }
            else
                Console.WriteLine("Contact doesn't exist");

        }
        private void EditContact()
        {
            Console.WriteLine("Enter full contact name");
            String ContactName = Console.ReadLine();

            if (AddressBook.ContainsKey(ContactName))
            {
                Console.WriteLine("enter choice");
                Console.WriteLine("1. First Name    2. Last Name    3. Address ");
                Console.WriteLine("4. City          5. State        6. Zip");
                Console.WriteLine("7. Phone number  8. Email");
                int Choice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter contact field:");
                String CotanctField = Console.ReadLine();

                switch (Choice)
                {
                    case 1:
                        AddressBook[ContactName]["First Name"] = CotanctField;
                        break;
                    case 2:
                        AddressBook[ContactName]["Last Name"] = CotanctField;
                        break;
                    case 3:
                        AddressBook[ContactName]["Address"] = CotanctField;
                        break;
                    case 4:
                        AddressBook[ContactName]["City"] = CotanctField;
                        break;
                    case 5:
                        AddressBook[ContactName]["State"] = CotanctField;
                        break;
                    case 6:
                        AddressBook[ContactName]["Zip"] = CotanctField;
                        break;
                    case 7:
                        AddressBook[ContactName]["Phone number"] = CotanctField;
                        break;
                    case 8:
                        AddressBook[ContactName]["Email"] = CotanctField;
                        break;
                }
            }
            else
                Console.WriteLine("contact doesn't exist");

        }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Address Book Program");
            AddressBookSystem addressBook = new AddressBookSystem();

            while (true)
            {
                Console.WriteLine("\n***** menu *****");
                Console.WriteLine("1. add contact   2. edit contact");
                Console.WriteLine("3. view contact");
                try
                {
                    switch (Convert.ToInt32(Console.ReadLine()))
                    {
                        case 1:
                            addressBook.AddContact();
                            break;
                        case 2:
                            addressBook.EditContact();
                            break;
                        case 3:
                            addressBook.ViewContact();
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
