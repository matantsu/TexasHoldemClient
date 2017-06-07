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
    public class GameManager : Changing, IGameManager
    {
        private IEnumerable<Game> games = new List<Game>();
        public IEnumerable<Game> Games
        {
            get { return games; }
            private set
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
            private set
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
            private set
            {
                if (spectatingGames != value)
                {
                    spectatingGames = value;
                    OnPropertyChanged("SpectatingGames");
                }
            }
        }

        private IUserManager userManager;
        private IFirebaseClient fb;
        private ApiManager api;
        public GameManager(IUserManager userManager, IFirebaseClient fb, ApiManager api)
        {
            this.userManager = userManager;
            this.fb = fb;
            this.api = api;

            userManager.PropertyChanged += UserManager_PropertyChanged;

            IObservable<IEnumerable<int>> activeIds = RxFirebase.FromPath<List<int>>(fb, "activeGames").Select(x => x != null ? x : new List<int>());
            IObservable<IEnumerable<Game>> games = RxFirebase.FromPaths<dynamic>(fb, activeIds.Select(ids => ids.Select(i => "games/" + i + "/publics"))).Select(gs => gs.Where(g => g != null).Select(ToGame));

            FillGames(games).Subscribe(gs =>
            {
                Games = gs;
            });
        }

        private IObservable<IEnumerable<Game>> FillGames(IObservable<IEnumerable<Game>> o)
        {
            return o.Select(gs => Observable.CombineLatest(gs.Select(g => FillGame(g.ID, g.PlayersCount).Select(players => 
                {
                    g.Players = players;
                    return g;
                })))).Switch();
        }

        private IObservable<IEnumerable<Player>> FillGame(int gameid, int players)
        {
            var paths = Observable.Return(Enumerable.Range(0, players)).Select(range => range.Select(i => "games/" + gameid + "/privates/allPlayers/" + i + "/publics"));
            return RxFirebase.FromPaths<dynamic>(fb, RxFirebase.Trace("pathsasd",paths))
                .Select(xs => xs.Where(x => x != null).Select(ToPlayer))
                .SelectMany(ps => Observable.CombineLatest(ps.Select(p => {
                    return userManager.CurrentUser == null || p.UserID != userManager.CurrentUser.UID ?
                        Observable.Return(p) :
                        RxFirebase.FromPath<dynamic>(fb, "games/" + gameid + "/privates/allPlayers/" + ps.ToList().FindIndex(x => x.UserID == p.UserID) + "/privates")
                        .Select(x => new Me { Hand = x.hand != null ? JsonConvert.DeserializeObject<IEnumerable<Card>>(x.hand.ToString()) : new List<Card>() }.Patch(p));
                }
                    )));
        }

        private void UserManager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentUser")
            {
                if (userManager.CurrentUser != null)
                {
                    IObservable<IEnumerable<int>> activeIds = RxFirebase.FromPath<List<int>>(fb, "users/" + userManager.CurrentUser.UID + "/publics/activeGamesIds").Select(x => x != null ? x : new List<int>());
                    IObservable<IEnumerable<Game>> activeGames = RxFirebase.FromPaths<dynamic>(fb, activeIds.Select(ids => ids.Select(i => "games/" + i + "/publics"))).Select(gs => gs.Where(g => g != null).Select(ToGame));

                    FillGames(activeGames).Subscribe(gs =>
                    {
                        ActiveGames = gs;
                    });

                    IObservable<IEnumerable<int>> spectatingIds = RxFirebase.FromPath<List<int>>(fb, "users/" + userManager.CurrentUser.UID + "/publics/spectatingGamesIds").Select(x => x != null ? x : new List<int>());
                    IObservable<IEnumerable<Game>> spectatingGames = RxFirebase.FromPaths<dynamic>(fb, activeIds.Select(ids => ids.Select(i => "games/" + i + "/publics"))).Select(gs => gs.Where(g => g != null).Select(ToGame));

                    FillGames(spectatingGames).Subscribe(gs =>
                    {
                        SpectatingGames = gs;
                    });
                }
                else
                {
                    ActiveGames = new List<Game>();
                    SpectatingGames = new List<Game>();
                }
            }
        }

        private Game ToGame(dynamic json)
        {
            return new Game
            {
                ID = json.gameId,
                Bet = json.bet,
                Buyin = json.buyin,
                IsOnRound = json.isPlaying,
                PlayersCount = json.playerAmount != null ? json.playerAmount : 0,
                InitialChips = json.initialChips,
                MaxPlayers = json.maxPlayers,
                MinBet = json.minBet,
                MinPlayers = json.minPlayers,
                IsSpectatingAllowed = json.spectatingAllowed,
                Type = json.type,
                League = json.league,
                OpenCards = json.openCards != null ? json.openCards.ToObject<List<Card>>() : new List<Card>(),
                Stage = json.stage,
                BigBlind = json.bigBlind != null ? json.bigBlind : 0,
                Pot = json.pot,
                SmallBet = json.smallBet != null ? json.smallBet : 0
            };
        }

        private Player ToPlayer(dynamic json)
        {
            return new Player
            {
                IsActive = json.isActive,
                Money = json.money,
                PlayerStatus = json.status != null ? json.status : PlayerStatus.Check,
                LastBet = json.lastBet != null ? json.lastBet : 0,
                Points = json.points,
                UserID = json.userId
            };
        }

        public async Task<int> Create(
            GameType gametype,
            string gameName,
            int buyin,
            int initialChips,
            int minBet,
            int minPlayers,
            int maxPlayers,
            bool spectatingAllowed)
        {
            var r = await api.CreateGame(
                gameName,
                (int)gametype,
                buyin,
                initialChips,
                minBet,
                minPlayers,
                maxPlayers,
                spectatingAllowed);
            return r.result;
        }

        public async Task Join(Game game)
        {
            await api.JoinGame(game.ID);
        }

        public async Task Leave(Game game)
        {
            await api.LeaveGame(game.ID);
        }

        public async Task Spectate(Game game)
        {
            await api.SpectateGame(game.ID);
        }

        public async Task StartRound(Game game)
        {
            await api.StartRound(game.ID);
        }

        public async Task Check(Game game)
        {
            int playerId = game.Players.ToList().FindIndex(x => x.UserID == userManager.CurrentUser.UID);
            await api.PlayerAction(playerId, game.ID, PlayerStatus.Check, 0);
        }

        public async Task Raise(Game game, int bet)
        {
            int playerId = game.Players.ToList().FindIndex(x => x.UserID == userManager.CurrentUser.UID);
            await api.PlayerAction(playerId, game.ID, PlayerStatus.Raise, bet);

        }

        public async Task Fold(Game game)
        {
            int playerId = game.Players.ToList().FindIndex(x => x.UserID == userManager.CurrentUser.UID);
            await api.PlayerAction(playerId, game.ID, PlayerStatus.Fold, 0);
        }

        private IDictionary<Game, IDisposable> gameListeners = new Dictionary<Game, IDisposable>(); 
        public Game Listen(int gameId)
        {
            Game game = new Game();
            var sub = RxFirebase.FromPath<dynamic>(fb,"games/" + gameId + "/publics")
                .Select(ToGame)
                .SelectMany(g => FillGame(g.ID,g.PlayersCount)
                    .Select(players => { g.Players = players; return g; }))
                .Subscribe(g => game.Patch(g));
            gameListeners.Add(game, sub);
            return game;
        }

        public void Dispose(Game game)
        {
            IDisposable disposable;
            gameListeners.TryGetValue(game, out disposable);
            if (disposable != null)
                disposable.Dispose();
        }
    }
}