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
        private readonly Regex ZipCodeRegex = new Regex(@"^[\\d]{3}\\s?[\\d]{3}$");

        Func<Regex, string, bool> IsValid = (reg, field) => reg.IsMatch(field);

        public bool ValidateFirstName(string FirstName)
        {
            try
            {
                if (FirstName.Equals(string.Empty))
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_EMPTY, "first name should not be empty");
                if (FirstName.Any(Char.IsDigit))
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_DIGIT_IN_NAME, "first name should not contain digits");
                if (FirstName.Length < 3)
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_LESSTHAN_MINIMUM_LENGTH, "first name should not be less than minimum length");
                if (Char.IsLower(FirstName[0]))
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_FIRST_LETTER_LOWERCASE, "first letter should not be lower case");
                if (FirstName.Substring(1).Any(Char.IsUpper))
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
                if (LastName.Any(Char.IsDigit))
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_DIGIT_IN_NAME, "last name should not contain digits");
                if (LastName.Length < 3)
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_LESSTHAN_MINIMUM_LENGTH, "last name should not be less than minimum length");
                if (Char.IsLower(LastName[0]))
                    throw new UserRegistrationException(UserRegistrationException.ExceptionType.ENTERED_FIRST_LETTER_LOWERCASE, "first letter should not be lower case");
                if (LastName.Substring(1).Any(Char.IsUpper))
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
                if (Username.Any(Char.IsPunctuation))
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
    }
}
