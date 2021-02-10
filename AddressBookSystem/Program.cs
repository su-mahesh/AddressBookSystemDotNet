using System;
using System.Collections.Generic;
using System.Linq;

namespace AddressBookSystem
{
    class AddressBookSystem
    {
        Dictionary<String, String> Contact;
        Dictionary<String, Dictionary<String, String>> AddressBook;
        Dictionary<String, List<Dictionary<String, String>>> CityAddressBook = new Dictionary<String, List<Dictionary<String, String>>>();
        Dictionary<String, List<Dictionary<String, String>>> StateAddressBook = new Dictionary<String, List<Dictionary<String, String>>>();
        List<Dictionary<String, String>> ContactList;
        Dictionary<String, Dictionary<String, Dictionary<String, String>>> AddressBookCollection = new Dictionary<string, Dictionary<String, Dictionary<String, String>>>();
        String CurrentAddressBookName = "default";
        public AddressBookSystem()
        {
            AddressBook = new Dictionary<string, Dictionary<String, String>>();
            AddressBookCollection.Add(CurrentAddressBookName, AddressBook);
        }

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
            Contact.TryGetValue("City", out String City);
            Contact.TryGetValue("State", out String State);
            string FullName = FirstName + " " + LastName;

            if (AddressBookCollection[CurrentAddressBookName].ContainsKey(FullName))
                    Console.WriteLine("contact already exist");
            else
            {                
                AddressBookCollection[CurrentAddressBookName].Add(FullName, Contact);
                if (!CityAddressBook.ContainsKey(City))
                {
                    ContactList = new List<Dictionary<String, String>>() { Contact };
                    CityAddressBook.Add(City, ContactList);
                }                   
                else
                {
                    CityAddressBook[City].Add(Contact);
                }

                if (!StateAddressBook.ContainsKey(State))
                {
                    ContactList = new List<Dictionary<String, String>>() { Contact };
                    StateAddressBook.Add(State, ContactList);
                }
                else
                {
                    StateAddressBook[State].Add(Contact);
                }
                Console.WriteLine("contact added\n");
            }                
        }

        void ShowContact(Dictionary<String, String> Contact)
        {
            Console.WriteLine("First Name: " + Contact["First Name"]);

            Console.WriteLine("Last Name:  " + Contact["Last Name"]);

            Console.WriteLine("Address:    " + Contact["Address"]);

            Console.WriteLine("City:       " + Contact["City"]);

            Console.WriteLine("State:      " + Contact["State"]);

            Console.WriteLine("Zip:        " + Contact["Zip"]);

            Console.WriteLine("Phone number:" + Contact["Phone number"]);

            Console.WriteLine("Email:       " + Contact["Email"]);

        }
        private void ViewContact()
        {
            Console.WriteLine("Enter full name:");
            String ContactName = Console.ReadLine();
            if (AddressBookCollection[CurrentAddressBookName].ContainsKey(ContactName))
            {
                Contact = AddressBookCollection[CurrentAddressBookName][ContactName];
                ShowContact(Contact);
            }
            else
                Console.WriteLine("Contact doesn't exist");

        }
        private void EditContactDetails()
        {
            Console.WriteLine("Enter full contact name");
            String ContactName = Console.ReadLine();

            if (AddressBookCollection[CurrentAddressBookName].ContainsKey(ContactName))
            {
            EditContact:
                Console.WriteLine("enter choice");
                Console.WriteLine("1. First Name    2. Last Name    3. Address ");
                Console.WriteLine("4. City          5. State        6. Zip");
                Console.WriteLine("7. Phone number  8. Email        9. exit");
                int Choice = Convert.ToInt32(Console.ReadLine());
                if (Choice > 0 && Choice < 9)
                {
                    Console.WriteLine("Enter contact field:");
                    String CotanctField = Console.ReadLine();
                    switch (Choice)
                    {
                        case 1:
                            AddressBookCollection[CurrentAddressBookName][ContactName]["First Name"] = CotanctField;
                            break;
                        case 2:
                            AddressBookCollection[CurrentAddressBookName][ContactName]["Last Name"] = CotanctField;
                            break;
                        case 3:
                            AddressBookCollection[CurrentAddressBookName][ContactName]["Address"] = CotanctField;
                            break;
                        case 4:
                            AddressBookCollection[CurrentAddressBookName][ContactName]["City"] = CotanctField;
                            break;
                        case 5:
                            AddressBookCollection[CurrentAddressBookName][ContactName]["State"] = CotanctField;
                            break;
                        case 6:
                            AddressBookCollection[CurrentAddressBookName][ContactName]["Zip"] = CotanctField;
                            break;
                        case 7:
                            AddressBookCollection[CurrentAddressBookName][ContactName]["Phone number"] = CotanctField;
                            break;
                        case 8:
                            AddressBookCollection[CurrentAddressBookName][ContactName]["Email"] = CotanctField;
                            break;
                      
                    }
                    String FullContactName = AddressBookCollection[CurrentAddressBookName][ContactName]["First Name"] + " " +
                                        AddressBookCollection[CurrentAddressBookName][ContactName]["Last Name"];
                  
                    AddressBookCollection[CurrentAddressBookName].Add(FullContactName, AddressBookCollection[CurrentAddressBookName][ContactName]);
                    AddressBookCollection[CurrentAddressBookName].Remove(ContactName);
                    ContactName = FullContactName;
                    Console.WriteLine("contact edited");
                    goto EditContact;
                }
            }
            else
                Console.WriteLine("contact doesn't exist");
        }
        private void DeleteContact()
        {
            Console.WriteLine("Enter contact name:");
            String ContactName = Console.ReadLine();
           
            try
            {
                if (AddressBook.ContainsKey(ContactName))
                {
                    Contact = AddressBook[ContactName];
                    AddressBook.Remove(ContactName);
                    string City = Contact["City"];
                    string State = Contact["State"];
                    CityAddressBook[City].Remove(Contact);
                    StateAddressBook[State].Remove(Contact);
                    if (CityAddressBook[City].Count().Equals(0))
                    {
                        CityAddressBook.Remove(City);
                    }
                    if (StateAddressBook[State].Count().Equals(0))
                    {
                        StateAddressBook.Remove(State);
                    }

                    Console.WriteLine("contact removed");
                }
                else
                    Console.WriteLine("contact doesn't exist");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
        private void CreateAddressBook()
        {
            AddressBook = new Dictionary<string, Dictionary<String, String>>();
            Console.WriteLine("Enter address book name:");
            string AddressBookName = Console.ReadLine();
            if (AddressBookCollection.ContainsKey(AddressBookName))
                Console.WriteLine("Address book already exist");
            else
            {
                AddressBookCollection.Add(AddressBookName, AddressBook);
                CurrentAddressBookName = AddressBookName;
                Console.WriteLine("Address book created");
            }
        }
        private void ChangeAddressBook()
        {
            Console.WriteLine("Enter address book name:");
            String AddressBookName = Console.ReadLine();
            if (AddressBookCollection.ContainsKey(AddressBookName))
            {
                CurrentAddressBookName = AddressBookName;
                Console.WriteLine("address book changed");
            }
            else
                Console.WriteLine("address book doesn't exist");
        }

        public void ViewPersons()
        {
            Console.WriteLine("1. by city  2. by state");
            int Choice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter field: ");
            string Field = Console.ReadLine();
            int ContactNumber = 0;
            try
            {
                switch (Choice)
                {
                    case 1:
                        foreach (Dictionary<String, String> Contact in CityAddressBook[Field])
                        {
                            Console.WriteLine("contact no: " + ++ContactNumber);
                            ShowContact(Contact);
                            Console.WriteLine();
                        }
                        break;
                    case 2:
                        foreach (Dictionary<String, String> Contact in StateAddressBook[Field])
                        {
                            Console.WriteLine("contact no: " + ++ContactNumber);
                            ShowContact(Contact);
                            Console.WriteLine();
                        }
                        break;
                }
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("no contact found");
            }
           

        }

        public void SearchPerson()
        {
            string City = "";
            string State = "";
            Console.WriteLine("enter person name:");
            string PersonName = Console.ReadLine();
            Console.WriteLine("1. search in city 2. search in state");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("enter city:");
                    City = Console.ReadLine();
                    break;
                case 2:
                    Console.WriteLine("enter state:");
                    State = Console.ReadLine();
                    break;
            }
            int ContactNumber = 0;

            foreach (Dictionary<String, Dictionary<String, String>> AddressBook in AddressBookCollection.Values)
            {
                foreach (Dictionary<String, String> Contact in AddressBook.Values)
                {
                    if ((Contact["First Name"] + " " + Contact["Last Name"]).Equals(PersonName) && (Contact["City"].Equals(City) || Contact["State"].Equals(State)))
                    {
                        Console.WriteLine("Contact no: " + ++ContactNumber);
                        ShowContact(Contact);
                        Console.WriteLine();
                    }
                }
            }

            if (ContactNumber.Equals(0))
            {
                Console.WriteLine("no person found");
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Address Book Program");
            AddressBookSystem AddressBookManager = new AddressBookSystem();

            while (true)
            {
                Console.WriteLine("\n******************** MENU *******************");
                Console.WriteLine("************ AddressBook: " + AddressBookManager.CurrentAddressBookName + " ***********");

                Console.WriteLine("1. add contact         2. edit contact");
                Console.WriteLine("3. view contact        4. delete contact");
                Console.WriteLine("5. create address book 6. change address book");
                Console.WriteLine("7. search person       8. view persons");
                try
                {
                    switch (Convert.ToInt32(Console.ReadLine()))
                    {
                        case 1: AddressBookManager.AddContact();
                            break;
                        case 2: AddressBookManager.EditContactDetails();
                            break;
                        case 3: AddressBookManager.ViewContact();
                            break;
                        case 4: AddressBookManager.DeleteContact();
                            break;
                        case 5: AddressBookManager.CreateAddressBook();
                            break;
                        case 6: AddressBookManager.ChangeAddressBook();
                            break;
                        case 7: AddressBookManager.SearchPerson();
                            break;
                        case 8: AddressBookManager.ViewPersons();
                            break;
                        default:
                            Console.WriteLine("wrong choice");
                            break;
                    }
                }
                catch (Exception e) {
                    Console.WriteLine("wrong input"+e);
                }                              
            }            
        }

    }
}
