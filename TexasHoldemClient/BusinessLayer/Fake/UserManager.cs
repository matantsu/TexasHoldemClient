using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TexasHoldemClient.BusinessLayer.Models;

namespace TexasHoldemClient.BusinessLayer.Fake
{
    class UserManager : Changing, IUserManager
    {
        private User currentUser;
        public User CurrentUser
        {
            get { return currentUser; }
            private set
            {
                if (currentUser != value)
                {
                    currentUser = value;
                    OnPropertyChanged("CurrentUser");
                }
            }
        }

        private string token;
        public string Token
        {
            get { return token; }
            private set
            {
                if (token != value)
                {
                    token = value;
                    OnPropertyChanged("Token");
                }
            }
        }

        public async Task<User> Login(string email, string password)
        {
            Thread.Sleep(1000);
            CurrentUser = new User("someid") { DisplayName = "Matan Tsuberi" };
            return CurrentUser;
        }

        public async Task Logout(string username, string password)
        {
            CurrentUser = null;
        }

        public Task<User> Register(string email, string username, string password)
        {
            return Login(email, password);
        }
    }
}
