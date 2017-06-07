using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TexasHoldemClient.BusinessLayer.Models;

namespace TexasHoldemClient.BusinessLayer.Fake
{
    public class GameManager : Changing, IGameManager
    {
        int lastId = 0;
        int lastPlayerId = 0;
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
                    OnPropertyChanged("ActiveGames");
                }
            }
        }
       
        public IEnumerable<Game> ActiveGames
        {
            get { return games.Where(g => getMe(g) != null); }
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

        public GameManager()
        {

            List<Game> games = new List<Game>();
            games.Add(new Game
            {
                ID = lastId++,
                Type = GameType.Limit,
                Name = "game1",
                Buyin = 20,
                InitialChips = 20,
                MinBet = 50,
                MinPlayers = 2,
                MaxPlayers = 5,
                IsSpectatingAllowed = true,
                Players = new List<Player>
                {
                    new Player {PlayerStatus = PlayerStatus.Check, UserID = "one"},
                    new Player {PlayerStatus = PlayerStatus.Check, UserID = "two"},
                    new Player {PlayerStatus = PlayerStatus.Fold, UserID = "three"},
                }
            });

            games.Add(new Game
            {
                ID = lastId++,
                Type = GameType.NoLimit,
                Name = "game2",
                Buyin = 20,
                InitialChips = 20,
                MinBet = 50,
                MinPlayers = 2,
                MaxPlayers = 5,
                IsSpectatingAllowed = false,
                Players = new List<Player>
                {
                    new Player { PlayerStatus = PlayerStatus.Check, UserID = "two"},
                    new Player { PlayerStatus = PlayerStatus.Raise, UserID = "three"},
                    new Player { PlayerStatus = PlayerStatus.Fold, UserID = "four"},
                }
            });

            games.Add(new Game
            {
                ID = lastId++,
                Type = GameType.PotLimit,
                Name = "game3",
                Buyin = 20,
                InitialChips = 20,
                MinBet = 50,
                MinPlayers = 2,
                MaxPlayers = 5,
                IsSpectatingAllowed = false,
                Players = new List<Player>
                {
                    new Player { PlayerStatus = PlayerStatus.Check, UserID = "four"},
                    new Player { PlayerStatus = PlayerStatus.Fold, UserID = "three"},
                }
            });

            Games = games;
        }

        private Player getMe(Game game)
        {
            return game.Players.FirstOrDefault(x => x.UserID == "one");
        }
        
        public async Task<int> Create(GameType gametype, string gameName, int buyin, int initialChips, int minBet, int minPlayers, int maxPlayers, bool spectatingAllowed)
        {
            Thread.Sleep(1000);
            List<Game> newGames = new List<Game>(Games);
            Game newGame = new Game
            {
                ID = lastId++,
                Type = gametype,
                Name = gameName,
                Buyin = buyin,
                InitialChips = initialChips,
                MinBet = minBet,
                MinPlayers = minPlayers,
                MaxPlayers = maxPlayers,
                IsSpectatingAllowed = spectatingAllowed
            };

            List<Player> players = new List<Player>();
            players.Add(new Player() { PlayerStatus = PlayerStatus.Check, UserID = "one" });
            newGame.Players = players;
            newGames.Add(newGame);

            Games = newGames;
            return newGame.ID;
        } 

        public async Task Check(Game game)
        {
            Thread.Sleep(1000);
            getMe(game).PlayerStatus = PlayerStatus.Check;
            Games = Games;
        }

        public async Task Fold(Game game)
        {
            Thread.Sleep(1000);
            getMe(game).PlayerStatus = PlayerStatus.Fold;
            Games = Games;
        }

        public async Task Raise(Game game, int bet)
        {
            Thread.Sleep(1000);
            getMe(game).PlayerStatus = PlayerStatus.Raise;
            getMe(game).Money = bet;
            Games = Games;
        }

        public async Task Join(Game game)
        {
            Thread.Sleep(1000);
            List<Player> newPlayers = new List<Player>(Games.First(x => x.ID == game.ID).Players);
            newPlayers.Add(new Player() { PlayerStatus = PlayerStatus.Check, UserID = "one" });
            Games.First(x => x.ID == game.ID).Players = newPlayers;
            Games = Games;
        }

        public async Task Spectate(Game game)
        {
            Thread.Sleep(1000);
            List<Game> spectatingGames = new List<Game>(SpectatingGames);
            spectatingGames.Add(game);
            SpectatingGames = spectatingGames;
        }

        public async Task Leave(Game game)
        {
            Thread.Sleep(1000);
            List<Game> spectatingGames = new List<Game>(SpectatingGames);
            spectatingGames.RemoveAll(x => x.ID == game.ID);
            SpectatingGames = spectatingGames;

            List<Game> games = new List<Game>(Games);
            games.RemoveAll(x => x.ID == game.ID);
            Games = games;
        }


        Dictionary<Game, PropertyChangedEventHandler> handlers = new Dictionary<Game, PropertyChangedEventHandler>();
        public Game Listen(int gameid)
        {
            Game game = Games.First(x => x.ID == gameid);

            PropertyChangedEventHandler handler = (a, b) =>
            {
                game.Patch(Games.First(x => x.ID == gameid));
            };
            handlers.Add(game, handler);

            this.PropertyChanged += handler;
            return game;
        }

        public void Dispose(Game game)
        {
            PropertyChangedEventHandler handler;
            handlers.TryGetValue(game,out handler);

            if(handler != null)
                this.PropertyChanged -= handler;
        }
    }
}
