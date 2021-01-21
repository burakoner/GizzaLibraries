﻿using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace Gizza.Data.Validation
{
    public static class StringValidators
    {
        public static bool IsValidUsername(string username)
        {
            string pattern;
            // start with a letter, allow letter or number, length between 5 to 20.
            pattern = @"^[a-zA-Z][a-zA-Z0-9_]{4,19}$";

            Regex regex = new Regex(pattern);
            return regex.IsMatch(username);
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

    }
}
