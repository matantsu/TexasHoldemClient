using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using TexasHoldemClient.BusinessLayer.Models;
using System.Windows;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Reactive.Disposables;
using TexasHoldemClient.BusinessLayer.api;
using Newtonsoft.Json;
using System.Windows.Threading;
using System.Windows.Documents;

namespace TexasHoldemClient.BusinessLayer
{
    public class GameManager : Changing
    {
        private IEnumerable<Game> games;
        public IEnumerable<Game> Games
        {
            get { return games; }
            set
            {
                if (games != value)
                {
                    games = value;
                    OnPropertyChanged("Games");
                }
            }
        }

        private IEnumerable<Game> activeGames = new List<Game>();
        public IEnumerable<Game> ActiveGames
        {
            get { return activeGames; }
            set
            {
                if (activeGames != value)
                {
                    activeGames = value;
                    OnPropertyChanged("ActiveGames");
                }
            }
        }

        private IEnumerable<Game> spectatingGames = new List<Game>();
        public IEnumerable<Game> SpectatingGames
        {
            get { return spectatingGames; }
            set
            {
                if (spectatingGames != value)
                {
                    spectatingGames = value;
                    OnPropertyChanged("SpectatingGames");
                }
            }
        }

        private UserManager userManager;
        private IFirebaseClient fb;
        private ServerApi api;
        public GameManager(UserManager userManager, IFirebaseClient fb, ServerApi api)
        {
            this.userManager = userManager;
            this.fb = fb;
            this.api = api;

            userManager.PropertyChanged += UserManager_PropertyChanged;

            RxFirebase.FromPath<IDictionary<string,dynamic>>(fb,"games").Select(xs => xs.Select(x => new Game { Key = x.Key, Bet = x.Value.bet })).Subscribe(games => Games = games);
        }

        private void UserManager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "CurrentUser")
            {
                if(userManager.CurrentUser != null)
                {

                    IObservable<IEnumerable<int>> activeIds = RxFirebase.FromPath<List<int>>(fb, "users/" + userManager.CurrentUser.Username + "/activeGamesIds").Select(x => x != null ? x : new List<int>());
                    IObservable<IDictionary<string, dynamic>> activeGamesOb = RxFirebase.FromPaths<dynamic>(fb, activeIds.Select(x => x.Select(id => "games/" + id)));
                    activeGamesOb.Select(xs => xs.Select(ToGame))
                        .SubscribeOn(Dispatcher.CurrentDispatcher)
                        .Subscribe(ls =>
                        {
                            ActiveGames = ls;
                        });

                    IObservable<IEnumerable<int>> spectatingIds = RxFirebase.FromPath<List<int>>(fb, "users/" + userManager.CurrentUser.Username + "/spectatingGamesIds").Select(x => x != null ? x : new List<int>());
                    IObservable<IDictionary<string, dynamic>> spectatingGamesOb = RxFirebase.FromPaths<dynamic>(fb, spectatingIds.Select(x => x.Select(id => "games/" + id)));
                    spectatingGamesOb.Select(xs => xs.Select(ToGame))
                        .SubscribeOn(Dispatcher.CurrentDispatcher)
                        .Subscribe(ls =>
                        {
                            SpectatingGames = ls;
                        });
                }
                else
                {
                    ActiveGames = new List<Game>();
                    SpectatingGames = new List<Game>();
                }
            }
        }

        private Game ToGame(KeyValuePair<string,dynamic> pair)
        {
            var json = pair.Value;
            List<dynamic> p = json.openCards != null ? json.allPlayers.ToObject<List<dynamic>>() : new List<dynamic>();
            List<Player> allPlayers = new List<Player>(p.FindAll(x => x != null).Select(ToPlayer));
            List<int> activeIds = json.activePlayers != null ? json.activePlayers.ToObject<List<int>>() : new List<int>();
            return new Game
            {
                Key = pair.Key,
                Bet = json.bet,
                Buyin = json.buyin,
                InitialChips = json.initialChips,
                MaxPlayers = json.maxPlayers,
                MinBet = json.minBet,
                MinPlayers = json.minPlayers,
                IsSpectatingAllowed = json.spectatingAllowed,
                Type = json.type,
                League = json.league,
                OpenCards = json.openCards != null ? json.openCards.ToObject<List<Card>>() : new List<Card>(),
                Stage = json.stage,
                Players = allPlayers,
                BigBlind = json.bigBlind != null ? json.bigBlind : 0,
                CurrentPlayer = allPlayers.Find(x => x.ID == json.currentPlayer),
                ActivePlayers = allPlayers.FindAll(x => activeIds.Contains(x.ID)),
                Pot = json.pot,
                SmallBet = json.smallBet != null ? json.smallBet : 0
            };
        }

        private Player ToPlayer(dynamic json)
        {
            return new Player
            {
                ID = json.playerId,
            };
        }

        public async Task Create(
            GameType gametype,
            int buyin,
            int initialChips,
            int minBet,
            int minPlayers,
            int maxPlayers,
            bool spectatingAllowed)
        {
            await api.CreateGame(
                userManager.CurrentUser.Username,
                userManager.CurrentUser.Password,
                gametype,
                buyin,
                initialChips,
                minBet,
                minPlayers,
                maxPlayers,
                spectatingAllowed);
        }

        public async Task Join(Game game)
        {
            await api.JoinGame(userManager.CurrentUser.Username, userManager.CurrentUser.Password, game.ID);
        }

        public async Task Leave(Game game)
        {
            await api.LeaveGame(userManager.CurrentUser.Username, userManager.CurrentUser.Password, game.ID);
        }

        public async Task Spectate(Game game)
        {
            await api.SpectateGame(userManager.CurrentUser.Username, userManager.CurrentUser.Password, game.ID);
        }
    }
}
