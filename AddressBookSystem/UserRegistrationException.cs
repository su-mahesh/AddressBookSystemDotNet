﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UserRegistrationNameSpace
{
    public class UserRegistrationException: Exception
    {
        public enum ExceptionType
        {
            ENTERED_LESSTHAN_MINIMUM_LENGTH,
            ENTERED_NULL,
            ENTERED_EMPTY,
            ENTERED_DIGIT_IN_NAME,
            ENTERED_INVALID_EMAIL_TLD,
            ENTERED_INVALID_EMAIL_USERNAME,
            ENTERED_DIGIT_IN_COUNTRY_TLD,
            ENTERED_PUNCTUATION_IN_START_OF_TLD,
            ENTERED_FIRST_LETTER_LOWERCASE,
            ENTERED_OTHERTHAN_FIRST_LETTER_UPPERCASE,
            ENTERED_SYMBOL,
            ENTERED_FIRST_DIGIT_ZERO,
            ENTERED_PUNCTUATION
        }
        public ExceptionType exceptionType;
        public UserRegistrationException(ExceptionType exceptionType, string message) : base(message)
        {
            this.exceptionType = exceptionType;
        }
    }
}
