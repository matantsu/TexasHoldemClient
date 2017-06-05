using Firebase.Auth;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldemClient.BusinessLayer.api;

namespace TexasHoldemClient.BusinessLayer
{
    class BL
    {
        public static readonly UserManager UserManager;
        public static readonly GameManager GameManager;

        static BL()
        {
            IFirebaseConfig config = new FireSharp.Config.FirebaseConfig
            {
                
                BasePath = Config.DatabaseRoot
            };
            IFirebaseClient fb = new FirebaseClient(config);
            var authProvider = new FirebaseAuthProvider(new Firebase.Auth.FirebaseConfig(Config.FirebaseAppKey));

            ServerApi api = RestService.For<ServerApi>(Config.ApiRoot);

            UserManager = new UserManager(api, authProvider);
            GameManager = new GameManager(UserManager, fb, api);
        }
    }
}
