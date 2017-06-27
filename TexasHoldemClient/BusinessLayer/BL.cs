using Firebase.Auth;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldemClient.BusinessLayer.api;
using TexasHoldemClient.BusinessLayer.Models;

namespace TexasHoldemClient.BusinessLayer
{
    public interface IGameManager : INotifyPropertyChanged
    {
        IEnumerable<Game> Games { get; }
        IEnumerable<Game> ActiveGames { get; }
        IEnumerable<Game> SpectatingGames { get; }

        IEnumerable<ChatMessage> Chat { get; }

        Task<int> Create(
            GameType gametype,
            string gameName,
            int buyin,
            int initialChips,
            int minBet,
            int minPlayers,
            int maxPlayers,
            bool spectatingAllowed);
        Task Join(Game game);
        Task Leave(Game game);
        Task Spectate(Game game);
        Task StartRound(Game game);
        Task Check(Game game);
        Task Raise(Game game, int bet);
        Task Fold(Game game);
        Game Listen(int gameid);
        void Dispose(Game game);
    }

    public interface IUserManager : INotifyPropertyChanged
    {
        string Token { get; }
        Models.User CurrentUser { get; }

        Task<Models.User> Register(string email, string username, string password);
        Task<Models.User> Login(string email, string password);
        Task Logout(string username, string password);
        Task ChangePassword(string password);
        Task ChangeEmail(string email);
    }

    public class BL
    {
        public static IUserManager UserManager { get; private set; }
        public static IGameManager GameManager { get; private set; }

        static BL()
        {
            real();
        }

        public static void fake()
        {
            UserManager = new Fake.UserManager();
            GameManager = new Fake.GameManager();
        }

        public static void real()
        {
            IFirebaseConfig config = new FireSharp.Config.FirebaseConfig
            {
                //AuthSecret = Config.FirebaseAppKey,
                BasePath = Config.DatabaseRoot
            };
            IFirebaseClient fb = new FirebaseClient(config);
            var authProvider = new FirebaseAuthProvider(new Firebase.Auth.FirebaseConfig(Config.FirebaseAppKey));

            ServerApi sapi = RestService.For<ServerApi>(Config.ApiRoot);

            UserManager = new UserManager(sapi, authProvider, fb);
            ApiManager api = new ApiManager(sapi, UserManager);
            GameManager = new GameManager(UserManager, fb, api);
        }
    }
}
