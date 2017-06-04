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
            IFirebaseConfig config = new FirebaseConfig
            {
                
                BasePath = Config.DATABASE_ROOT
            };
            IFirebaseClient fb = new FirebaseClient(config);

            ServerApi api = RestService.For<ServerApi>(Config.API_ROOT);

            UserManager = new UserManager(api);
            GameManager = new GameManager(UserManager, fb, api);
        }
    }
}
