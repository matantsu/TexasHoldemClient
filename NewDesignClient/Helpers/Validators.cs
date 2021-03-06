﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NewDesignClient.Helpers
{
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

        
    }
}
