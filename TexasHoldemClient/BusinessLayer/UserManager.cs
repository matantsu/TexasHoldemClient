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
    public class UserManager : Changing
    {
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
                }
            }
        }

        private ServerApi userApi;
        private FirebaseAuthProvider authProvider;

        public UserManager(ServerApi api, FirebaseAuthProvider authProvider)
        {
            this.userApi = api;
            this.authProvider = authProvider;
        }

        public async Task Login(string email, string password)
        {
            var cr = new PromptCodeReceiver();

            var result = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets { ClientId = Config.GoogleClientId, ClientSecret = Config.GoogleClientSecret },
                new[] { "email", "profile" },
                "user",
                CancellationToken.None);

            if (result.Token.IsExpired(SystemClock.Default))
            {
                await result.RefreshTokenAsync(CancellationToken.None);
            }

            var auth = new FirebaseAuthProvider(new FirebaseConfig(Config.FirebaseAppKey));
            var data = await auth.SignInWithOAuthAsync(FirebaseAuthType.Google, result.Token.AccessToken);
            CurrentUser = new Models.User { PhotoURL = data.User.PhotoUrl, DisplayName = data.User.DisplayName };
        }

        public async Task Logout(string username, string password)
        {
            CurrentUser = null;
        }
    }
}
