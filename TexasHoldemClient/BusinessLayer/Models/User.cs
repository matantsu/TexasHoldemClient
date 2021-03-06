﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldemClient.BusinessLayer.Models
{
    public class User : Changing
    {
        private string displayName;
        public string DisplayName
        {
            get { return displayName; }
            set
            {
                if (displayName != value)
                {
                    displayName = value;
                    OnPropertyChanged("DisplayName");
                }
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged("Email");
                }
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        private int wallet;
        public int Wallet
        {
            get { return wallet; }
            set
            {
                if (wallet != value)
                {
                    wallet = value;
                    OnPropertyChanged("Wallet");
                }
            }
        }

        private int league;
        public int League
        {
            get { return league; }
            set
            {
                if (league != value)
                {
                    league = value;
                    OnPropertyChanged("League");
                }
            }
        }

        public string UID { get; private set; }

        public User(string uid)
        {
            this.UID = uid;
        }
    }
}
