using Firebase.Auth;
using Firebase.Database;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

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
        private UserManager()
        {
            
        }
        public static UserManager instance = new UserManager();

        private User user = new User();
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

        public string FirebaseApiKey { get; private set; }

        public async void Logout()
        {
            this.User = null;
        }

        public void Register()
        {
            this.User = new User() { Name = "new user" };
        }

        string GoogleClientId = "989213145723-78seudgbebld842o21up0t7nml3fffhu.apps.googleusercontent.com"; // https://console.developers.google.com/apis/credentials
        string GoogleClientSecret = "K9n32CzRfQeOXZX-miDWxrde"; // https://console.developers.google.com/apis/credentials
        string FirebaseAppKey = "AIzaSyDNeJein6-7c543frBjRY-YMj30GV-9XZI"; // https://console.firebase.google.com/

        public async Task Login(string v)
        {
            try
            {
                var cr = new PromptCodeReceiver();

                var result = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new ClientSecrets { ClientId = GoogleClientId, ClientSecret = GoogleClientSecret },
                    new[] { "email", "profile" },
                    "user",
                    CancellationToken.None);

                if (result.Token.IsExpired(SystemClock.Default))
                {
                    await result.RefreshTokenAsync(CancellationToken.None);
                }

                this.FetchFirebaseData(result.Token.AccessToken, FirebaseAuthType.Google);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private async void FetchFirebaseData(string accessToken, FirebaseAuthType authType)
        {
            try
            {
                // Convert the access token to firebase token
                var auth = new FirebaseAuthProvider(new FirebaseConfig(FirebaseAppKey));
                var data = await auth.SignInWithOAuthAsync(authType, accessToken);
                this.User = new User() { Name = data.User.DisplayName };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
