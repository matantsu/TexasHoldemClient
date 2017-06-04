using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TexasHoldemClient.BusinessLayer.api;
using TexasHoldemClient.BusinessLayer.Models;

namespace TexasHoldemClient.BusinessLayer
{
    public class UserManager : Changing
    {
        private User currentUser = null;
        public User CurrentUser
        {
            get { return currentUser; }
            set
            {
                if(currentUser != value)
                {
                    currentUser = value;
                    OnPropertyChanged("CurrentUser");
                }
            }
        }

        private ServerApi userApi;
        public UserManager(ServerApi api)
        {
            this.userApi = api;
        }

        public async Task Login(string username, string password)
        {
            CurrentUser = new User() { Username = username, Password = password };
        }

        public async Task Logout(string username, string password)
        {
            CurrentUser = null;
        }

        public async Task Register(string username, string password, string email)
        {
            try
            {
                await userApi.Register(username, password, email);
                MessageBox.Show("Registered");
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }

        public async Task UploadPhoto(Stream fileStream)
        {
            
        }
    }
}
