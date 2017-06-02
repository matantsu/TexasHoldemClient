using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TexasHoldemClient.Exceptions;
using TexasHoldemClient.Models;

namespace TexasHoldemClient.bl
{

    class UserManager : Observable
    {
        private UserManager()
        {
            
        }
        public static UserManager instance = new UserManager();
        private User user;


        public User User
        {
            get { return user; }
            set
            {
                if(user != value)
                {
                    user = value;
                    OnPropertyChanged("User");
                }
            }
        }
        

        public async void Logout()
        {
            this.User = null;
        }
        
        public async Task<bool> Register(string username, string email, string password)
        {
            bool task = await ServerAPI.serverApi.Register(username, email, password);
            return task;
        }

        public async Task<User> Login(string username, string password)
        {
            user = await ServerAPI.serverApi.Login(username, password);
            return user;
        }


    }
}
