using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldemClient.bl
{

    class User : Observable
    {
        private string name = "Hello";
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
    }

    class UserManager : Observable
    {
        private UserManager() { }
        public static UserManager instance = new UserManager();

        private User user = new User();
        public User User
        {
            get { return user; }
            set
            {
                if (user != value)
                {
                    user = value;
                    OnPropertyChanged("User");
                }
            }
        }

        public void Logout()
        {
            this.User = null;
        }

        internal void Register()
        {
            this.User = new User() { Name = "new user" };
        }

        internal void Login(string v)
        {
            this.User = new User() { Name = v };
        }
    }
}
