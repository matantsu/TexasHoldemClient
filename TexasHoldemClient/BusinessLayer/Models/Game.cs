using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldemClient.BusinessLayer.Models
{
    public enum GameType
    {
        Limit = 0,
        NoLimit = 1,
        PotLimit = 2
    }

    public enum GameStage
    {
        Preflop,
        Flop,
        Turn,
        River
    }

    public enum CardType
    {
        Spade,
        Club,
        Heart,
        Diamond,
    }

    public enum CardRank
    {
        Ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
    }

    //Value Object
    public class GameAction
    {
        public readonly PlayerStatus status;
        public readonly int? bet;
        
        public GameAction(PlayerStatus status, int? bet)
        {
            this.status = status;
            this.bet = bet;
        }
    }

    // Value Object
    public class Card
    {
        public readonly CardType type;
        public readonly CardRank number;
        public Card(CardType type, CardRank number)
        {
            this.type = type;
            this.number = number;
        }
    }

    public class Game : Changing
    {
        
        private string key;
        public string Key
        {
            get { return key; }
            set
            {
                if (key != value)
                {
                    key = value;
                    OnPropertyChanged("Key");
                    OnPropertyChanged("ID");
                }
            }
        }
        
        public int ID
        {
            get {
                int i;
                int.TryParse(key.Split('/').Last(), out i);
                return i;
            }
        }

        private GameStage stage;
        public GameStage Stage
        {
            get { return stage; }
            set
            {
                if (stage != value)
                {
                    stage = value;
                    OnPropertyChanged("Stage");
                }
            }
        }

        private IEnumerable<Card> openCards;
        public IEnumerable<Card> OpenCards
        {
            get { return openCards; }
            set
            {
                if (openCards != value)
                {
                    openCards = value;
                    OnPropertyChanged("OpenCards");
                }
            }
        }

        private int bet;
        public int Bet
        {
            get { return bet; }
            set
            {
                if (bet != value)
                {
                    bet = value;
                    OnPropertyChanged("Bet");
                }
            }
        }

        private GameType type;
        public GameType Type
        {
            get { return type; }
            set
            {
                if (type != value)
                {
                    type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        private int buyin;
        public int Buyin
        {
            get { return buyin; }
            set
            {
                if (buyin != value)
                {
                    buyin = value;
                    OnPropertyChanged("Buyin");
                }
            }
        }

        private int league;
        public int League
        {
            get { return league; }
            set
            {
                if (league != value)
                {
                    league = value;
                    OnPropertyChanged("League");
                }
            }
        }

        private int initialChips;
        public int InitialChips
        {
            get { return initialChips; }
            set
            {
                if (initialChips != value)
                {
                    initialChips = value;
                    OnPropertyChanged("InitialChips");
                }
            }
        }

        private int minBet;
        public int MinBet
        {
            get { return minBet; }
            set
            {
                if (minBet != value)
                {
                    minBet = value;
                    OnPropertyChanged("MinBet");
                }
            }
        }

        private int minPlayers;
        public int MinPlayers
        {
            get { return minPlayers; }
            set
            {
                if (minPlayers != value)
                {
                    minPlayers = value;
                    OnPropertyChanged("MinPlayers");
                }
            }
        }

        private int maxPlayers;
        public int MaxPlayers
        {
            get { return maxPlayers; }
            set
            {
                if (maxPlayers != value)
                {
                    maxPlayers = value;
                    OnPropertyChanged("MaxPlayers");
                }
            }
        }

        private bool isSpectatingAllowed;
        public bool IsSpectatingAllowed
        {
            get { return isSpectatingAllowed; }
            set
            {
                if (isSpectatingAllowed != value)
                {
                    isSpectatingAllowed = value;
                    OnPropertyChanged("SpectatingAllowed");
                }
            }
        }

        private IEnumerable<Player> players;
        public IEnumerable<Player> Players
        {
            get { return players; }
            set
            {
                if (players != value)
                {
                    players = value;
                    OnPropertyChanged("Players");
                }
            }
        }

        private Player currentPlayer;
        public Player CurrentPlayer
        {
            get { return currentPlayer; }
            set
            {
                if (currentPlayer != value)
                {
                    currentPlayer = value;
                    OnPropertyChanged("CurrentPlayer");
                }
            }
        }

        private IEnumerable<Player> activePlayers;
        public IEnumerable<Player> ActivePlayers
        {
            get { return activePlayers; }
            set
            {
                if (activePlayers != value)
                {
                    activePlayers = value;
                    OnPropertyChanged("ActivePlayers");
                }
            }
        }

        private int smallBet;
        public int SmallBet
        {
            get { return smallBet; }
            set
            {
                if (smallBet != value)
                {
                    smallBet = value;
                    OnPropertyChanged("SmallBet");
                }
            }
        }

        private int pot;
        public int Pot
        {
            get { return pot; }
            set
            {
                if (pot != value)
                {
                    pot = value;
                    OnPropertyChanged("Pot");
                }
            }
        }

        private int bigBlind;
        public int BigBlind
        {
            get { return bigBlind; }
            set
            {
                if (bigBlind != value)
                {
                    bigBlind = value;
                    OnPropertyChanged("BigBlind");
                }
            }
        }

        public void Patch(Game x)
        {
            this.Stage = x.Stage;
            this.OpenCards = x.OpenCards;
            this.Bet = x.Bet;
            this.Type = x.Type;
            this.Buyin = x.Buyin;
            this.League = x.League;
            this.InitialChips = x.InitialChips;
            this.MinBet = x.MinBet;
            this.MinPlayers = x.MinPlayers;
            this.MaxPlayers = x.MaxPlayers;
            this.IsSpectatingAllowed = x.IsSpectatingAllowed;
            this.Players = x.Players;
            this.CurrentPlayer = x.CurrentPlayer;
            this.ActivePlayers = x.ActivePlayers;
            this.SmallBet = x.SmallBet;
            this.Pot = x.Pot;
            this.BigBlind = x.BigBlind;
        }
    }
}
