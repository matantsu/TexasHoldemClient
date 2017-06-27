using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TexasHoldemClient.Helpers
{
    public class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return string.IsNullOrWhiteSpace((value ?? "").ToString())
                ? new ValidationResult(false, "Field is required.")
                : ValidationResult.ValidResult;
        }
    }


    public static class Validators
    {
        public static bool isEmailValid(string email)
        {
            return !(email == null || email.Length == 0 || !Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"));
        }


        public static bool isUsaernameValid(string username)
        {
            return !(username == null || username.Length == 0);
        }

        public static bool isPasswordConfirmValid(string pass, string confirmPass)
        {
            return !(pass != confirmPass || pass == null || pass == "");
        }

    }
}
