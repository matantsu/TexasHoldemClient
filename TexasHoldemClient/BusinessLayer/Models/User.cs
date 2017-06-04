using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldemClient.BusinessLayer.Models
{
    public class User : Changing
    {
        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged("DisplayName");
                }
            }
        }

        private string photoURL;
        public string PhotoURL
        {
            get { return photoURL; }
            set
            {
                if (photoURL != value)
                {
                    photoURL = value;
                    OnPropertyChanged("PhotoURL");
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
    }
}
