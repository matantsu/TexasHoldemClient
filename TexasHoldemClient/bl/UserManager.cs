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
        private LinkedList<User> registeredUsers = new LinkedList<User>();


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
            if(!ExceptionControl.LoginAllow) throw new RegestrationException();
            registeredUsers.AddFirst(new User(username, email, password));
            return true;
        }

        public async Task<User> Login(string username, string password)
        {
            if (!ExceptionControl.LoginAllow) throw new LoginException();
            
            foreach (User u in registeredUsers)
            {
                //Console.WriteLine("username: {0}, password: {1}", u.Username , u.Password);
                if (u.Username == username && u.Password == password)
                {
                    this.User = u;
                    return u;
                }
            }
            throw new LoginException();

        }


    }
}
