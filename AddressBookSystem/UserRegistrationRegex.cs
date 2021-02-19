using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace UserRegistrationNameSpace
{
    public class UserRegistrationRegex
    {
        private readonly Regex FirstNameRegex = new Regex(@"^[A-Z][a-z]{2,}$");
        private readonly Regex LastNameRegex = new Regex(@"^[A-Z][a-zA-Z]{2,}$");
        private readonly Regex EmailAddressRegex = new Regex(@"^[a-zA-Z0-9]+([._+-][a-zA-Z0-9]+)*@[a-zA-Z0-9]+.[a-zA-Z]{2,4}([.][a-zA-Z]{2,})?$");
        private readonly Regex MobileNumberRegex = new Regex(@"^[0-9]{2,3}\s[1-9][0-9]{9}$");
        private readonly Regex ZipCodeRegex = new Regex(@"^[1-9][0-9]{2}[ ]?[0-9]{3}$");
        private readonly Regex AddressRegex = new Regex(@"^[0-9A-Za-z]{5,25}$");
        private readonly Regex CityRegex = new Regex(@"^[0-9A-Za-z]{2,25}$");
        private readonly Regex StateRegex = new Regex(@"^[0-9A-Za-z]{2,25}$");

        Func<Regex, string, bool> IsValid = (reg, field) => reg.IsMatch(field);

        public bool ValidateFirstName(string FirstName)
        {
            try
            {
                if (FirstName.Equals(string.Empty))
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_EMPTY, "first name should not be empty");
                if (FirstName.Any(char.IsDigit))
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_DIGIT_IN_NAME, "first name should not contain digits");
                if (FirstName.Length < 3)
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_LESSTHAN_MINIMUM_LENGTH, "first name should not be less than minimum length");
                if (char.IsLower(FirstName[0]))
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_FIRST_LETTER_LOWERCASE, "first letter should not be lower case");
                if (FirstName[1..].Any(char.IsUpper))
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_OTHERTHAN_FIRST_LETTER_UPPERCASE, "other than first letter should not be upper case");
                return IsValid(FirstNameRegex, FirstName);

            }
            catch (NullReferenceException)
            {
                throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_NULL, "first name should not be null");
            }            
        }
        public bool ValidateLastName(string LastName)
        {
            try
            {
                if (LastName.Equals(string.Empty))
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_EMPTY, "last name should not be empty");
                if (LastName.Any(char.IsDigit))
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_DIGIT_IN_NAME, "last name should not contain digits");
                if (LastName.Length < 3)
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_LESSTHAN_MINIMUM_LENGTH, "last name should not be less than minimum length");
                if (char.IsLower(LastName[0]))
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_FIRST_LETTER_LOWERCASE, "first letter should not be lower case");
                if (LastName[1..].Any(char.IsUpper))
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_OTHERTHAN_FIRST_LETTER_UPPERCASE, "other than first letter should not be upper case");
                return IsValid(LastNameRegex, LastName);
            }
            catch (NullReferenceException)
            {
                throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_NULL, "last name should not be null");
            }            
        }
        public bool ValidateEmailAddress(string EmailAddress)
        {
            try
            {
                if (EmailAddress.Equals(string.Empty))
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_EMPTY, "email address should not be empty");
                string Username = EmailAddress.Substring(0, 1);
                if (Username.Any(char.IsPunctuation))
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_INVALID_EMAIL_USERNAME, "email address username should not start with spacial character");
                if (EmailAddress.Length < 6)
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_LESSTHAN_MINIMUM_LENGTH, "email address should not be less than minimum lengthe");
                return IsValid(EmailAddressRegex, EmailAddress);
            }
            catch (NullReferenceException)
            {
                throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_NULL, "email address should not be null");
            }
        }

        internal bool ValidateZipCode(string ZipCode)
        {
            try
            {
                if (ZipCode.Length.Equals(0))
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_EMPTY, "zip code should not be empty");
                if (ZipCode.Length < 6)
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_LESSTHAN_MINIMUM_LENGTH, "zip code should not be less than minimum length");
                if (ZipCode[0].Equals("0"))
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_FIRST_DIGIT_ZERO, "zip code should not start with");
                return IsValid(ZipCodeRegex, ZipCode);
            }
            catch (NullReferenceException)
            {
                throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_NULL, "zip code should not be null");
            }
        }

        public bool ValidateMobileNumber(string MobileNumber)
        {
            try
            {
                if (MobileNumber.Length.Equals(0))
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_EMPTY, "mobile number should not be empty");
                if (MobileNumber.Length < 13)
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_LESSTHAN_MINIMUM_LENGTH, "mobile number should not be less than minimum length");
                return IsValid(MobileNumberRegex, MobileNumber);
            }
            catch (NullReferenceException)
            { 
                throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_NULL, "mobile number should not be null");
            }
        }
        internal bool ValidateAddress(string Address)
        {
            try
            {
                if (Address.Equals(string.Empty))
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_EMPTY, "address should not be empty");
                if (Address.Length < 5)
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_LESSTHAN_MINIMUM_LENGTH, "address should not be less than minimum length");
                if (Address.Any(char.IsSymbol))
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_SYMBOL, "address should not contain any symbols");
                if (Address.Any(char.IsPunctuation))
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_PUNCTUATION, "address should not contain any punctuation");
                return IsValid(AddressRegex, Address);
            }
            catch (NullReferenceException)
            {
                throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_NULL, "address should not be null");
            }     
        }
        internal bool ValidateCity(string City)
        {
            try
            {
                if (City.Equals(string.Empty))
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_EMPTY, "city should not be empty");
                if (City.Any(char.IsDigit))
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_DIGIT_IN_NAME, "city should not contain digits");
                if (City.Length < 2)
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_LESSTHAN_MINIMUM_LENGTH, "city should not be less than minimum length");
                if (City.Any(char.IsSymbol))
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_SYMBOL, "city should not contain any symbols");
                return IsValid(CityRegex, City);
            }
            catch (NullReferenceException)
            {
                throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_NULL, "city should not be null");
            }
        }
        internal bool ValidateState(string State)
        {
            try
            {
                if (State.Equals(string.Empty))
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_EMPTY, "state should not be empty");
                if (State.Any(char.IsDigit))
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_DIGIT_IN_NAME, "state should not contain digits");
                if (State.Length < 2)
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_LESSTHAN_MINIMUM_LENGTH, "state should not be less than minimum length");
                if (State.Any(char.IsSymbol))
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_SYMBOL, "state should not contain any symbols");
                return IsValid(StateRegex, State);
            }
            catch (NullReferenceException)
            {
                throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_NULL, "state should not be null");
            }
        }
    }
}
