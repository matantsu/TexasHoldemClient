using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldemClient.bl;

namespace TexasHoldemClient.Models
{

    class User : Observable
    {
        private string username;
        private string password;
        private string email;

        public User()
        {
        }

        public User(string username,string email, string password)
        {
            this.username = username;
            this.password = password;
            this.email = email;
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
    }

}
