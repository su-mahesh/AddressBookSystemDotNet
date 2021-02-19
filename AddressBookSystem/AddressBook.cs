using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UserRegistrationNameSpace;

namespace AddressBookSystemNameSpace
{/// <summary>
/// AddressBookSystem containing variables and methods
/// </summary>
    public class AddressBookSystem
    {      
        /// <summary>
        /// collection variables
        /// </summary>
        Dictionary<string, string> Contact;
        SortedDictionary<string, Dictionary<string, string>> AddressBook;
        SortedDictionary<string, List<Dictionary<string, string>>> CityAddressBook = new SortedDictionary<string, List<Dictionary<string, string>>>();
        SortedDictionary<string, List<Dictionary<string, string>>> StateAddressBook = new SortedDictionary<string, List<Dictionary<string, string>>>();
        Dictionary<string, SortedDictionary<string, Dictionary<string, string>>> AddressBookCollection = new Dictionary<string, SortedDictionary<string, Dictionary<string, string>>>();
        public string CurrentAddressBookName = "default";
        public string[] ContactFieldType = { "First Name", "Last Name", "Address", "City", "State", "Zip", "Phone number", "Email" };

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressBookSystem"/> class.
        /// </summary>
        public AddressBookSystem()
        {
            AddressBook = new SortedDictionary<string, Dictionary<string, string>>();
            AddressBookCollection.Add(CurrentAddressBookName, AddressBook);
        }

        /// <summary>
        /// Edits the contact details.
        /// </summary>
        public void EditContactDetails(string ContactName, int FieldType, string Field)
        {
            SortedDictionary<string, List<Dictionary<string, string>>> addressbook = CityAddressBook;
            Dictionary<string, string> Contact = AddressBookCollection[CurrentAddressBookName][ContactName];
            string TempField = Contact[ContactFieldType[FieldType]];
            switch (FieldType)
            {
                case 3:
                    addressbook = CityAddressBook;                   
                    break;
                case 4:
                    addressbook = StateAddressBook;                   
                    break;
            }
            Contact[ContactFieldType[FieldType]] = Field;
            if (FieldType.Equals(0) || FieldType.Equals(1))
            {
                string first_name = Contact[ContactFieldType[0]];
                string last_name = Contact[ContactFieldType[1]];
                AddressBookCollection[CurrentAddressBookName].Add(first_name + " " + last_name, Contact);
                AddressBookCollection[CurrentAddressBookName].Remove(ContactName);
            }            

            if (FieldType.Equals(3) || FieldType.Equals(4))
            {
                addressbook[TempField].Remove(Contact);
                if (addressbook[TempField].Count.Equals(0))
                {
                    addressbook.Remove(TempField);
                }
                if (addressbook.ContainsKey(Contact[ContactFieldType[FieldType]]))
                {
                    addressbook[Contact[ContactFieldType[FieldType]]].Add(Contact);
                }
                else
                {
                    addressbook.Add(Contact[ContactFieldType[FieldType]], new List<Dictionary<string, string>>() { Contact });
                }
            }
        }
        /// <summary>
        /// Deletes the contact.
        /// </summary>
        public void DeleteContact(string ContactName)
        {
            if (IsContactPresent(ContactName))
            {
                try
                {
                    Dictionary<string, string> Contact = AddressBookCollection[CurrentAddressBookName][ContactName];
                    string City = Contact[ContactFieldType[3]];
                    string State = Contact[ContactFieldType[4]];
                    AddressBookCollection[CurrentAddressBookName].Remove(ContactName);
                    CityAddressBook[City].Remove(Contact);
                    StateAddressBook[State].Remove(Contact);
                    if (CityAddressBook[City].Count().Equals(0))
                        CityAddressBook.Remove(City);

                    if (StateAddressBook[State].Count().Equals(0))
                        StateAddressBook.Remove(State);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }                
        }

        /// <summary>
        /// Creates the address book with name
        /// </summary>
        public void CreateAddressBook(string AddressBookName)
        {
            AddressBook = new SortedDictionary<string, Dictionary<string, string>>();
                AddressBookCollection.Add(AddressBookName, AddressBook);
                CurrentAddressBookName = AddressBookName;
        }
        
        public bool IsContactPresent(string PersonName)
        {
            return AddressBookCollection[CurrentAddressBookName].ContainsKey(PersonName);
        }
        /// <summary>
        /// Adds the contact.
        /// </summary>
        /// <param name="ContactFields">The contact fields.</param>
        public void AddContact(string[] ContactFields)
        {
            Contact = new Dictionary<string, string>();
            if (ContactFieldType.Length.Equals(ContactFields.Length))
            {
                for (int i = 0; i < ContactFieldType.Length; i++)
                {
                    Contact.Add(ContactFieldType[i], ContactFields[i]);
                }
                string City = Contact[ContactFieldType[3]];
                string State = Contact[ContactFieldType[4]];
                string PersonName = Contact[ContactFieldType[0]] + " " + Contact[ContactFieldType[1]];

                if (IsContactPresent(PersonName))
                    Console.WriteLine("contact already exist");
                else
                {
                    AddressBookCollection[CurrentAddressBookName].Add(PersonName, Contact);
                    if (!CityAddressBook.ContainsKey(City))
                    {
                        CityAddressBook.Add(City, new List<Dictionary<string, string>>() { Contact });
                    }
                    else
                        CityAddressBook[City].Add(Contact);

                    if (!StateAddressBook.ContainsKey(State))
                    {
                        StateAddressBook.Add(State, new List<Dictionary<string, string>>() { Contact });
                    }
                    else
                        StateAddressBook[State].Add(Contact);

                    Console.WriteLine("contact added\n");
                }
            }
        }

        public string GetContactField(string ContactName, int FieldType)
        {
            return AddressBookCollection[CurrentAddressBookName][ContactName][ContactFieldType[FieldType]];
        }
        public string[] GetAllContactField(string ContactName)
        {
            return AddressBookCollection[CurrentAddressBookName][ContactName].Values.ToArray();
        }

        public bool CheckAddressBookExist(string AddressBookName)
        {
            return AddressBookCollection.ContainsKey(AddressBookName);
        }

        internal void SetCurrentAddressBook(string AddressBookName)
        {
            CurrentAddressBookName = AddressBookName;
        }

        internal List<Dictionary<string, string>> GetPersonsFromCity(string City, string PersonName)
        {
            try
            {
                return CityAddressBook[City].Where(l => l[ContactFieldType[0]] +" "+l[ContactFieldType[1]] ==  PersonName).ToList();      
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }
        internal List<Dictionary<string, string>> GetPersonsFromState(string State, string PersonName)
        {
            try
            {
                return StateAddressBook[State].Where(l => l[ContactFieldType[0]] + " " + l[ContactFieldType[1]] == PersonName).ToList();
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        internal List<Dictionary<string, string>> GetAllContactsFromCity(string City)
        {
            CityAddressBook.TryGetValue(City, out List<Dictionary<string, string>> Contacts);
            return Contacts;
        }

        internal List<Dictionary<string, string>> GetAllContactsFromState(string State)
        {           
            StateAddressBook.TryGetValue(State, out List<Dictionary<string, string>> Contacts);
            return Contacts;
        }

        internal int GetContactsCountByCity(string City)
        {
            try
            {
                return CityAddressBook[City].Count;
            }
            catch (KeyNotFoundException)
            {
                return 0;
            }           
        }

        internal int GetContactsCountByState(string State)
        {
            try
            {
                return StateAddressBook[State].Count;
            }
            catch (KeyNotFoundException)
            {
                return 0;
            }           
        }

        internal List<Dictionary<string, string>> GetAddressBookSortedByCity()
        {
            var Contacts = AddressBookCollection[CurrentAddressBookName].Values.ToList();
                Contacts.Sort((contact1, contact2) => contact1[ContactFieldType[3]].CompareTo(contact2[ContactFieldType[3]]));
            return Contacts;
        }

        internal void PrintContacts(List<Dictionary<string, string>> Contacts)
        {
            int ContactNumber = 0;
            if (Contacts != null)
            {
                foreach (var contact in Contacts)
                {
                    Console.WriteLine("Contact No: " + ++ContactNumber);
                    foreach (var field in contact)
                    {
                        Console.WriteLine(field.Key.PadRight(12) + ": " + field.Value);
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("no person found");
            }
        }

        internal List<Dictionary<string, string>> GetCurrentAddressBook()
        {
            return AddressBookCollection[CurrentAddressBookName].Values.ToList();
        }

        internal List<Dictionary<string, string>> GetAddressBookSortedByState()
        {
            var Contacts = AddressBookCollection[CurrentAddressBookName].Values.ToList();
            Contacts.Sort((contact1, contact2) => contact1[ContactFieldType[4]].CompareTo(contact2[ContactFieldType[4]]));
            return Contacts;
        }

        internal List<Dictionary<string, string>> GetAddressBookSortedByZip()
        {
            var Contacts = AddressBookCollection[CurrentAddressBookName].Values.ToList();
            Contacts.Sort((contact1, contact2) => contact1[ContactFieldType[5]].CompareTo(contact2[ContactFieldType[5]]));
            return Contacts;
        }

        internal string ReadFile(string FileName)
        {
            if (File.Exists(FileName))
            {
                return File.ReadAllText(FileName);
            }
            return null;                
        }

        internal bool WriteToFile(string FileName)
        {
            if (File.Exists(FileName))
            {
                using (StreamWriter sr = File.CreateText(FileName))
                {
                    foreach (var addressbook in AddressBookCollection)
                    {
                        int ContactNo = 1;
                        sr.WriteLine("Address book: " + addressbook.Key);
                        foreach (var Contact in addressbook.Value.Values)
                        {
                            sr.WriteLine("\nContact no: " + ContactNo++);
                            foreach (var CotactField in Contact)
                            {
                                sr.WriteLine(CotactField.Key.PadRight(12) + ": " + CotactField.Value);
                            }
                        }
                        sr.WriteLine();
                    }
                    sr.Close();
                    return true;
                }
            }
            return false;
        }

        public void WriteToCsvFile(string csvFile)
        {
            var csv = new StringBuilder();
            for (int i = 0; i < ContactFieldType.Length; i++)
            {
                csv.Append(ContactFieldType[i]);
                if (i != ContactFieldType.Length - 1)
                {
                    csv.Append(",");
                }
            }
            csv.Append("\n");
            AddressBookCollection[CurrentAddressBookName].Values.ToList().
                ForEach(Contact => { Contact.Values.ToList().
                ForEach(ContactField => { csv.Append(ContactField); 
                    if (Contact.Last().Value != ContactField) { csv.Append(",");}
                    else
                        csv.Append("\n");
                }); });
            using StreamWriter sw = File.CreateText(csvFile);
            sw.Write(csv.ToString());
            Console.WriteLine("wrote to csv file");
        }

        internal void ReadFromCsvFile(string csvFile)
        {
            if (File.Exists(csvFile))
            {
                using StreamReader sr = File.OpenText(csvFile);
                string[] header = sr.ReadLine().Split(",");
                string Lines = "";
                int ContactNo = 1;
                string[] field;
                while ((Lines = sr.ReadLine() ) != null)
                {
                    Console.WriteLine("Contact no: " + ContactNo++);
                    field = Lines.Split(",");
                    for (int i = 0; i < header.Length; i++)
                    {
                        Console.WriteLine(header[i].PadRight(12) + ": " + field[i]);
                    }
                    Console.WriteLine();
                }
            }
        }
    }
    public class Program
    {
        static int NumContactFields = 8;
        static string[] ContactFields = new string[NumContactFields];
        static string input;
        static UserRegistrationRegex userRegistrationRegex = new UserRegistrationRegex();
        static AddressBookSystem AddressBookManager = new AddressBookSystem();
        static string FileName = @"C:/Users/Mahesh Kangude/source/repos/AddressBookSystem/AddressBookSystem/AddressBook.txt";
        static string CsvFile = @"C:/Users/Mahesh Kangude/source/repos/AddressBookSystem/AddressBookSystem/AddressBook.csv";

        static void AddContact()
        {
            try
            {
                Console.WriteLine("First Name:");
                input = Console.ReadLine();
                //      input = "Mahh";
                if (userRegistrationRegex.ValidateFirstName(input))
                    ContactFields[0] = input;
                Console.WriteLine("Last Name:");
                //   input = Console.ReadLine();
                input = "Kan";
                if (userRegistrationRegex.ValidateLastName(input))
                    ContactFields[1] = input;
                Console.WriteLine("Address:");
                //   input = Console.ReadLine();
                input = "mohitewadi";
                if (userRegistrationRegex.ValidateAddress(input))
                    ContactFields[2] = input;
                Console.WriteLine("City:");
                //       input = Console.ReadLine();
                input = "pune";
                if (userRegistrationRegex.ValidateCity(input))
                    ContactFields[3] = input;
                Console.WriteLine("State:");
                //    input = Console.ReadLine();
                input = "Mahar";
                if (userRegistrationRegex.ValidateState(input))
                    ContactFields[4] = input;
                Console.WriteLine("Zip:");
               // input = Console.ReadLine();
                  input = "666 777";
                if (userRegistrationRegex.ValidateZipCode(input))
                    ContactFields[5] = input;
                Console.WriteLine("Phone number:");
                // input = Console.ReadLine();
                input = "91 9938888883";
                if (userRegistrationRegex.ValidateMobileNumber(input))
                    ContactFields[6] = input;
                Console.WriteLine("Email:");
                //  input = Console.ReadLine();
                input = "dde@fe.fe";
                if (userRegistrationRegex.ValidateEmailAddress(input))
                    ContactFields[7] = input;
                AddressBookManager.AddContact(ContactFields);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        static void EditContactDetails()
        {
            Console.WriteLine("Enter full contact name");
            string ContactName = Console.ReadLine();
            if (AddressBookManager.IsContactPresent(ContactName))
            {
                EditContact:
                string first_name = AddressBookManager.GetContactField(ContactName, 0);
                string last_name = AddressBookManager.GetContactField(ContactName, 1);

                Console.WriteLine("enter choice");
                Console.WriteLine("1. First Name    2. Last Name    3. Address ");
                Console.WriteLine("4. City          5. State        6. Zip");
                Console.WriteLine("7. Phone number  8. Email        9. exit");
                int Choice = Convert.ToInt32(Console.ReadLine());
                if (Choice > 0 && Choice < 9)
                {
                    Console.WriteLine("Enter contact field:");
                    string ContactField = Console.ReadLine();
                    try
                    {
                        switch (Choice)
                        {
                            case 1:
                                if (userRegistrationRegex.ValidateFirstName(ContactField))
                                    first_name = ContactField;
                                break;
                            case 2:
                                if (userRegistrationRegex.ValidateLastName(ContactField))
                                    last_name = ContactField;
                                break;
                            case 3:
                                userRegistrationRegex.ValidateAddress(ContactField);
                                break;
                            case 4:
                                userRegistrationRegex.ValidateCity(ContactField);
                                break;
                            case 5:
                                userRegistrationRegex.ValidateState(ContactField);
                                break;
                            case 6:
                                userRegistrationRegex.ValidateZipCode(ContactField);
                                break;
                            case 7:
                                userRegistrationRegex.ValidateMobileNumber(ContactField);
                                break;
                            case 8:
                                userRegistrationRegex.ValidateEmailAddress(ContactField);
                                break;
                        }
                        AddressBookManager.EditContactDetails(ContactName, Choice - 1, ContactField);
                        if (Choice.Equals(1) || Choice.Equals(2))
                        {
                            ContactName = first_name + " " + last_name;
                        }
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
            {
                Console.WriteLine("contact doesn't exist");
            }
        }
        static void ViewContact()
        {
            Console.WriteLine("Enter full name:");
            string ContactName = Console.ReadLine();
            if (AddressBookManager.IsContactPresent(ContactName))
            {
                for (int field = 0; field < AddressBookManager.ContactFieldType.Length; field++)
                {
                    Console.WriteLine(AddressBookManager.ContactFieldType[field].PadRight(12) + ": " + AddressBookManager.GetContactField(ContactName, field));
                }
            }
            else
                Console.WriteLine("Contact doesn't exist");
        }
        static void DeleteContact()
        {
            Console.WriteLine("Enter contact name:");
            string ContactName = Console.ReadLine();

            AddressBookManager.DeleteContact(ContactName);
        }
        static void CreateAddressBook()
        {
            Console.WriteLine("Enter address book name:");
            string AddressBookName = Console.ReadLine();
            if (!AddressBookManager.CheckAddressBookExist(AddressBookName))
            {
                AddressBookManager.CreateAddressBook(AddressBookName);
                Console.WriteLine("Address book created");
            }
            else
                Console.WriteLine("address book already exist");
        }
        static string GetCurrentAddressBookName()
        {
            return AddressBookManager.CurrentAddressBookName;
        }

        static void ChangeAddressBook()
        {
            Console.WriteLine("Enter address book name:");
            string AddressBookName = Console.ReadLine();
            if (AddressBookManager.CheckAddressBookExist(AddressBookName))
            {
                AddressBookManager.SetCurrentAddressBook(AddressBookName);
                Console.WriteLine("current address book changed");
            }
            else
                Console.WriteLine("address book doesn't exist");
        }

        /// <summary>
        /// Searches the person across city and state
        /// </summary>
        static void SearchPerson()
        {
            List<Dictionary<string, string>> Contacts = new List<Dictionary<string, string>>();
            Console.WriteLine("enter person name:");
            string PersonName = Console.ReadLine();
            Console.WriteLine("1. search in city 2. search in state");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("enter city:");
                    string City = Console.ReadLine();
                    Contacts = AddressBookManager.GetPersonsFromCity(City, PersonName);
                    break;
                case 2:
                    Console.WriteLine("enter state:");
                    string State = Console.ReadLine();
                    Contacts = AddressBookManager.GetPersonsFromState(State, PersonName);
                    break;
            }
            int ContactNumber = 0;
            if (Contacts != null)
            {
                foreach (var contact in Contacts)
                {
                    Console.WriteLine("Contact No: " + ++ContactNumber);
                    foreach (var field in contact)
                    {
                        Console.WriteLine(field.Key.PadRight(12) + ": " + field.Value);
                    }
                }
            }
            else
            {
                Console.WriteLine("no person found");
            }



        }
        private static void ViewAddressBook()
        {
            var Contacts = AddressBookManager.GetCurrentAddressBook();
            AddressBookManager.PrintContacts(Contacts);
        }

        private static void SortAddressBook()
        {
            var Contacts = new List<Dictionary<string, string>>();
            Console.WriteLine("sort by 1. name 2. city  3. state  4. zip");
            int Choice = Convert.ToInt32(Console.ReadLine());
            switch (Choice)
            {
                case 1:
                    Contacts = AddressBookManager.GetCurrentAddressBook();
                    break;
                case 2:
                    Contacts = AddressBookManager.GetAddressBookSortedByCity();
                    break;
                case 3:
                    Contacts = AddressBookManager.GetAddressBookSortedByState();
                    break;
                case 4:
                    Contacts = AddressBookManager.GetAddressBookSortedByZip();
                    break;
            }
            AddressBookManager.PrintContacts(Contacts);
        }

        private static void CountContacts()
        {
            Console.WriteLine("1. count by city  2. count by state");
            int Choice = Convert.ToInt32(Console.ReadLine());
            string field = "";
            int PersonsCount = 0;

            switch (Choice)
            {
                case 1:
                    Console.WriteLine("enter city: ");
                    field = Console.ReadLine();
                    PersonsCount = AddressBookManager.GetContactsCountByCity(field);
                    break;
                case 2:
                    Console.WriteLine("enter state: ");
                    field = Console.ReadLine();
                    PersonsCount = AddressBookManager.GetContactsCountByState(field);
                    break;
            }
            Console.WriteLine("number of persons in " + field + ": " + PersonsCount);
        }

        private static void ViewAllPersons()
        {
            Console.WriteLine("1. by city  2. by state");
            int Choice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter field: ");
            string Field = Console.ReadLine();
            var Contacts = new List<Dictionary<string, string>>();
            switch (Choice)
            {
                case 1:
                    Contacts = AddressBookManager.GetAllContactsFromCity(Field);
                    break;
                case 2:
                    Contacts = AddressBookManager.GetAllContactsFromState(Field);
                    break;
            }
            if (Contacts == null)
            {
                Console.WriteLine("no contact found");
            }
            else
            {
                AddressBookManager.PrintContacts(Contacts);
            }
        }
        private static void WriteAddressBookToFile()
        {
            if (AddressBookManager.WriteToFile(FileName))
            {
                Console.WriteLine("wrote to file");
            }
        }
        private static void ReadAddressBookFromFile()
        {
            string output = AddressBookManager.ReadFile(FileName);
            Console.WriteLine(output);
        }
        static void Main()
        {          
            Console.WriteLine("Welcome to Address Book Program");
            while (true)
            {
                Console.WriteLine("\n******************** MENU *******************");
                Console.WriteLine("*********** AddressBook: " + GetCurrentAddressBookName() + " ***********");

                Console.WriteLine(" 1. add contact          2. edit contact");
                Console.WriteLine(" 3. view contact         4. delete contact");
                Console.WriteLine(" 5. create address book  6. change address book");
                Console.WriteLine(" 7. search person        8. view persons");
                Console.WriteLine(" 9. count contacts      10. view address book");
                Console.WriteLine("11. sort address book   12. write address book to file");
                Console.WriteLine("13. read address book from file");
                Console.WriteLine("14. write to csv file");
                Console.WriteLine("15. read csv file");
                try
                {
                    switch (Convert.ToInt32(Console.ReadLine()))
                    {
                        case 1:
                            AddContact();
                            break;
                        case 2:
                            EditContactDetails();
                            break;
                        case 3:
                            ViewContact();
                            break;
                        case 4:
                            DeleteContact();
                            break;
                        case 5:
                            CreateAddressBook();
                            break;
                        case 6:
                            ChangeAddressBook();
                            break;
                        case 7:
                            SearchPerson();
                            break;
                        case 8:
                            ViewAllPersons();
                            break;
                        case 9:
                            CountContacts();
                            break;
                        case 10:
                            ViewAddressBook();
                            break;
                        case 11:
                            SortAddressBook();
                            break;
                        case 12:
                            WriteAddressBookToFile();
                            break;
                        case 13:
                            ReadAddressBookFromFile();
                            break;
                        case 14:
                            WriteAddressBookToCsvFile();
                            break;
                        case 15:
                            ReadAddressBookFromCsvFile();
                            break;
                        default:
                            Console.WriteLine("wrong choice");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("wrong input " + e);
                }
            }
        }

        public static void ReadAddressBookFromCsvFile()
        {
            if (File.Exists(CsvFile))
            {
                AddressBookManager.ReadFromCsvFile(CsvFile);
            }
        }

        private static void WriteAddressBookToCsvFile()
        {
            if (File.Exists(CsvFile))
            {
                AddressBookManager.WriteToCsvFile(CsvFile);
            }  
        }      
    }
}
