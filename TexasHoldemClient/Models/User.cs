using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldemClient.bl;

namespace TexasHoldemClient.Models
{

    public class User : Observable
    {
        private string username;
        private string password;
        private string email;
        private int wallet;
        private int league;


        public User()
        {
        }

        public User(string username,string email, string password, int wallet, int league)
        {
            this.username = username;
            this.password = password;
            this.email = email;
            this.wallet = wallet;
            this.league = league;
        }

        public string Username
        {
            get { return username; }
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged("Username");
                }
            }
        }

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
    }

}
