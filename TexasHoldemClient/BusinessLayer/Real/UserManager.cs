using Firebase.Auth;
using FireSharp.Interfaces;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TexasHoldemClient.BusinessLayer.api;
using TexasHoldemClient.BusinessLayer.Models;

namespace TexasHoldemClient.BusinessLayer
{
    public class UserManager : Changing, IUserManager
    {
        private string email;
        private string password;

        private Models.User currentUser = null;
        public Models.User CurrentUser
        {
            get { return currentUser; }
            set
            {
                if(currentUser != value)
                {
                    currentUser = value;
                    OnPropertyChanged("CurrentUser");
                    OnPropertyChanged("Token");
                }
            }
        }

        public string Token { get; private set; }

        private ServerApi api;
        private FirebaseAuthProvider authProvider;
        private IFirebaseClient fb;

        public UserManager(ServerApi api, FirebaseAuthProvider authProvider, IFirebaseClient fb)
        {
            this.api = api;
            this.fb = fb;
            this.authProvider = authProvider;
        }

        public async Task<Models.User> Register(string email, string username, string password)
        {
            var data = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password, username, false);
            await api.Register(email, username, password,data.FirebaseToken);
            CurrentUser = ToUser(data);
            Token = data.FirebaseToken;
            return CurrentUser;
        }

        public async Task<Models.User> Login(string email, string password)
        {
            var data = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
            CurrentUser = ToUser(data);
            Token = data.FirebaseToken;
            this.email = email;
            this.password = password;
            return CurrentUser;
        }

        public async Task Logout(string username, string password)
        {
            CurrentUser = null;
            Token = null;
            email = null;
            password = null;
        }

        private Models.User ToUser(FirebaseAuthLink l)
        {
            return new Models.User(l.User.LocalId)
            {
                DisplayName = l.User.DisplayName,
            };
        }
    }
}
