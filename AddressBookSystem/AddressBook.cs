using System;
using System.Collections.Generic;
using System.Linq;
using UserRegistrationNameSpace;

namespace AddressBookSystem
{/// <summary>
/// AddressBookSystem containing variables and methods
/// </summary>
    class AddressBookSystem
    {
        UserRegistrationRegex userRegistrationRegex = new UserRegistrationRegex();
        /// <summary>
        /// collection variables
        /// </summary>
        Dictionary<string, string> Contact;
        SortedDictionary<string, Dictionary<string, string>> SortedAddressBook;
        SortedDictionary<string, Dictionary<string, string>> AddressBook;
        SortedDictionary<string, List<Dictionary<string, string>>> CityAddressBook = new SortedDictionary<string, List<Dictionary<string, string>>>();
        SortedDictionary<string, List<Dictionary<string, string>>> StateAddressBook = new SortedDictionary<string, List<Dictionary<string, string>>>();
        List<Dictionary<string, string>> ContactList;
        Dictionary<string, SortedDictionary<string, Dictionary<string, string>>> AddressBookCollection = new Dictionary<string, SortedDictionary<string, Dictionary<string, string>>>();
        string CurrentAddressBookName = "default";
        string[] ContactFields = {"First Name", "Last Name", "Address", "City", "State", "Zip", "Phone number", "Email"};

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressBookSystem"/> class.
        /// </summary>
        public AddressBookSystem()
        {
            AddressBook = new SortedDictionary<string, Dictionary<string, string>>();
            AddressBookCollection.Add(CurrentAddressBookName, AddressBook);
        }
        /// <summary>
        /// Adds the contact.
        /// </summary>
        private void AddContact()
        {
            try
            {
                string input;
                Console.WriteLine("add contact");
                Contact = new Dictionary<string, string>();

                Console.WriteLine("First Name:");
                input = Console.ReadLine();
                if (userRegistrationRegex.ValidateFirstName(input))
                    Contact.Add(ContactFields[0], input);

                Console.WriteLine("Last Name:");
                input = Console.ReadLine();
                if (userRegistrationRegex.ValidateLastName(input))
                    Contact.Add(ContactFields[1], input);

                Console.WriteLine("Address:");
                Contact.Add(ContactFields[2], Console.ReadLine());
                Console.WriteLine("City:");
                Contact.Add(ContactFields[3], Console.ReadLine());
                Console.WriteLine("State:");
                Contact.Add(ContactFields[4], Console.ReadLine());
                Console.WriteLine("Zip:");
                input = Console.ReadLine();
                if (userRegistrationRegex.ValidateZipCode(input))
                    Contact.Add(ContactFields[5], input);

                Console.WriteLine("Phone number:");
                input = Console.ReadLine();
                if (userRegistrationRegex.ValidateMobileNumber(input))
                    Contact.Add(ContactFields[6], input);

                Console.WriteLine("Email:");
                input = Console.ReadLine();
                if (userRegistrationRegex.ValidateEmailAddress(input))
                    Contact.Add(ContactFields[7], input);
                    
                Contact.TryGetValue(ContactFields[0], out string FirstName);
                Contact.TryGetValue(ContactFields[1], out string LastName);
                Contact.TryGetValue(ContactFields[3], out string City);
                Contact.TryGetValue(ContactFields[4], out string State);
                string FullName = FirstName + " " + LastName;

                if (AddressBookCollection[CurrentAddressBookName].ContainsKey(FullName))
                        Console.WriteLine("contact already exist");
                else
                {                
                    AddressBookCollection[CurrentAddressBookName].Add(FullName, Contact);
                    if (!CityAddressBook.ContainsKey(City))
                    {
                        ContactList = new List<Dictionary<string, string>>() { Contact };
                        CityAddressBook.Add(City, ContactList);
                    }                   
                    else
                        CityAddressBook[City].Add(Contact);

                    if (!StateAddressBook.ContainsKey(State))
                    {
                        ContactList = new List<Dictionary<string, string>>() { Contact };
                        StateAddressBook.Add(State, ContactList);
                    }
                    else
                        StateAddressBook[State].Add(Contact);

                    Console.WriteLine("contact added\n");

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        /// <summary>
        /// Shows the contact detail
        /// </summary>
        /// <param name="Contact">The contact.</param>
        void ShowContact(Dictionary<string, string> Contact)
        {
            Console.WriteLine("First Name: " + Contact[ContactFields[0]]);
            Console.WriteLine("Last Name:  " + Contact[ContactFields[1]]);
            Console.WriteLine("Address:    " + Contact[ContactFields[2]]);
            Console.WriteLine("City:       " + Contact[ContactFields[3]]);
            Console.WriteLine("State:      " + Contact[ContactFields[4]]);
            Console.WriteLine("Zip:        " + Contact[ContactFields[5]]);
            Console.WriteLine("Phone number:" + Contact[ContactFields[6]]);
            Console.WriteLine("Email:       " + Contact[ContactFields[7]]);
        }
        /// <summary>
        /// Views the contact.
        /// </summary>
        private void ViewContact()
        {
            Console.WriteLine("Enter full name:");
            string ContactName = Console.ReadLine();
            if (AddressBookCollection[CurrentAddressBookName].ContainsKey(ContactName))
            {
                Contact = AddressBookCollection[CurrentAddressBookName][ContactName];
                ShowContact(Contact);
            }
            else
                Console.WriteLine("Contact doesn't exist");

        }
        /// <summary>
        /// Edits the contact details.
        /// </summary>
        private void EditContactDetails()
        {
            Console.WriteLine("Enter full contact name");
            string ContactName = Console.ReadLine();

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
                    string CotanctField = Console.ReadLine();
                    try
                    {
                        switch (Choice)
                        {
                            case 1:
                                if (userRegistrationRegex.ValidateFirstName(CotanctField))
                                    AddressBookCollection[CurrentAddressBookName][ContactName][ContactFields[0]] = CotanctField;
                                break;
                            case 2:
                                if (userRegistrationRegex.ValidateLastName(CotanctField))
                                    AddressBookCollection[CurrentAddressBookName][ContactName][ContactFields[1]] = CotanctField;
                                break;
                            case 3:
                                AddressBookCollection[CurrentAddressBookName][ContactName][ContactFields[2]] = CotanctField;
                                break;
                            case 4:
                                AddressBookCollection[CurrentAddressBookName][ContactName][ContactFields[3]] = CotanctField;
                                break;
                            case 5:
                                AddressBookCollection[CurrentAddressBookName][ContactName][ContactFields[4]] = CotanctField;
                                break;
                            case 6:
                                if (userRegistrationRegex.ValidateZipCode(CotanctField))
                                    AddressBookCollection[CurrentAddressBookName][ContactName][ContactFields[5]] = CotanctField;
                                break;
                            case 7:
                                if (userRegistrationRegex.ValidateMobileNumber(CotanctField))
                                    AddressBookCollection[CurrentAddressBookName][ContactName][ContactFields[6]] = CotanctField;
                                break;
                            case 8:
                                if (userRegistrationRegex.ValidateEmailAddress(CotanctField))
                                    AddressBookCollection[CurrentAddressBookName][ContactName][ContactFields[7]] = CotanctField;
                                break;
                        }
                        AddressBookCollection[CurrentAddressBookName][ContactName][ContactFields[Choice]] = CotanctField;
                        string FullContactName = AddressBookCollection[CurrentAddressBookName][ContactName][ContactFields[0]] + " " +
                                            AddressBookCollection[CurrentAddressBookName][ContactName][ContactFields[1]];

                        AddressBookCollection[CurrentAddressBookName].Add(FullContactName, AddressBookCollection[CurrentAddressBookName][ContactName]);
                        AddressBookCollection[CurrentAddressBookName].Remove(ContactName);
                        ContactName = FullContactName;
                        Console.WriteLine("contact edited");
                    }
                    catch (UserRegistrationException e)
                    {
                        Console.WriteLine(e);
                    }                                       
                    goto EditContact;
                }
            }
            else
                Console.WriteLine("contact doesn't exist");
        }
        /// <summary>
        /// Deletes the contact.
        /// </summary>
        private void DeleteContact()
        {
            Console.WriteLine("Enter contact name:");
            string ContactName = Console.ReadLine();
           
            try
            {
                if (AddressBook.ContainsKey(ContactName))
                {
                    Contact = AddressBook[ContactName];
                    AddressBook.Remove(ContactName);
                    string City = Contact[ContactFields[3]];
                    string State = Contact[ContactFields[4]];
                    CityAddressBook[City].Remove(Contact);
                    StateAddressBook[State].Remove(Contact);
                    if (CityAddressBook[City].Count().Equals(0))
                        CityAddressBook.Remove(City);

                    if (StateAddressBook[State].Count().Equals(0))
                        StateAddressBook.Remove(State);

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

        /// <summary>
        /// Creates the address book with name
        /// </summary>
        private void CreateAddressBook()
        {
            AddressBook = new SortedDictionary<string, Dictionary<string, string>>();
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
            string AddressBookName = Console.ReadLine();
            if (AddressBookCollection.ContainsKey(AddressBookName))
            {
                CurrentAddressBookName = AddressBookName;
                Console.WriteLine("address book changed");
            }
            else
                Console.WriteLine("address book doesn't exist");
        }
        /// <summary>
        /// Views the persons by city or state
        /// </summary>
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
                        foreach (Dictionary<string, string> Contact in CityAddressBook[Field])
                        {
                            Console.WriteLine("contact no: " + ++ContactNumber);
                            ShowContact(Contact);
                            Console.WriteLine();
                        }
                        break;
                    case 2:
                        foreach (Dictionary<string, string> Contact in StateAddressBook[Field])
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
        /// <summary>
        /// Searches the person across city and state
        /// </summary>
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

            foreach (SortedDictionary<string, Dictionary<string, string>> AddressBook in AddressBookCollection.Values)
            {
                foreach (Dictionary<string, string> Contact in AddressBook.Values)
                {
                    if ((Contact[ContactFields[0]] + " " + Contact[ContactFields[2]]).Equals(PersonName) && (Contact[ContactFields[3]].Equals(City) || Contact[ContactFields[4]].Equals(State)))
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
        /// <summary>
        /// Counts the contacts city or state
        /// </summary>
        private void CountContacts()
        {
            Console.WriteLine("1. count by city 2. count by state");
            int Choice = Convert.ToInt32(Console.ReadLine());
            string field = "";
            int PersonsCount = 0;
            try
            {
                switch (Choice)
                {
                    case 1:
                        Console.WriteLine("enter city: ");
                        field = Console.ReadLine();
                        PersonsCount = CityAddressBook[field].Count;
                        break;
                    case 2:
                        Console.WriteLine("enter state: ");
                        field = Console.ReadLine();
                        PersonsCount = StateAddressBook[field].Count;
                        break;
                }
                Console.WriteLine("number of persons in " + field + ": " + PersonsCount);
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("not contacts found");
            }
        }
        /// <summary>
        /// Converts to string. override ToString()
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (SortedAddressBook == null)
                SortedAddressBook = new SortedDictionary<string, Dictionary<string, string>>(AddressBookCollection[CurrentAddressBookName]);
            string ContactList = "";
            int number = 1;
            foreach (var Contacts in SortedAddressBook.Values)
            {
                ContactList += "contact no: " + number++ + "\n";
                foreach (var Contact in Contacts)
                {
                    ContactList += Contact.Key + ": " + Contact.Value + "\n";
                }
                ContactList += "\n";
            }
            SortedAddressBook = null;
            return ContactList;
        }
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        static void Main()
        {
            Console.WriteLine("Welcome to Address Book Program");
            AddressBookSystem AddressBookManager = new AddressBookSystem();

            while (true)
            {
                Console.WriteLine("\n******************** MENU *******************");
                Console.WriteLine("*********** AddressBook: " + AddressBookManager.CurrentAddressBookName + " ***********");

                Console.WriteLine(" 1. add contact          2. edit contact");
                Console.WriteLine(" 3. view contact         4. delete contact");
                Console.WriteLine(" 5. create address book  6. change address book");
                Console.WriteLine(" 7. search person        8. view persons");
                Console.WriteLine(" 9. count contacts      10. view address book");
                Console.WriteLine("11. sort address book");
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
                        case 9: AddressBookManager.CountContacts();
                            break;
                        case 10: Console.WriteLine(AddressBookManager);
                            break;
                        case 11: AddressBookManager.SortAddressbook();
                            Console.WriteLine(AddressBookManager);
                            break;
                        default: Console.WriteLine("wrong choice");
                            break;
                    }
                }catch (Exception) {
                    Console.WriteLine("wrong input");
                }                              
            }            
        }
        /// /<summary>
        /// Sorts the addressbook.                                                                                  
        /// </summary>
        private void SortAddressbook()
        {
            Console.WriteLine("sort by 1. name 2. city  3. state  4. zip");
            int Choice = Convert.ToInt32(Console.ReadLine());
            string SortBy = "";
            switch (Choice)
            {
                case 1:
                    SortedAddressBook = new SortedDictionary<string, Dictionary<string, string>>(AddressBookCollection[CurrentAddressBookName]);
                    break;
                case 2:
                    SortBy = ContactFields[3];
                    break;
                case 3:
                    SortBy = ContactFields[4];
                    break;
                case 4:
                    SortBy = ContactFields[5];
                    break;
            }
            if (SortedAddressBook == null)
            {
                SortedAddressBook = new SortedDictionary<string, Dictionary<string, string>>();
                foreach (var item in AddressBookCollection[CurrentAddressBookName])
                {
                    SortedAddressBook.Add(item.Value[SortBy], item.Value);
                }
            }
        }
    }
}
