using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldemClient.bl;

namespace TexasHoldemClient.Models
{
    public enum GamePolicy
    {
        Limit,
        NOLimit,
        PotLimit
    }

    public class Game : Observable
    {
        private GamePolicy gp;
        public GamePolicy GamePolicy
        {
            get { return gp; }
            set
            {
                if (gp != value)
                {
                    gp = value;
                    OnPropertyChanged("GamePolicy");
                }
            }
        }

        private int buyIn;
        public int BuyIn
        {
            get { return buyIn; }
            set
            {
                if (buyIn != value)
                {
                    buyIn = value;
                    OnPropertyChanged("BuyIn");
                }
            }
        }

        private int chipsPerPlayer;
        public int ChipsPerPlayer
        {
            get { return chipsPerPlayer; }
            set
            {
                if (chipsPerPlayer != value)
                {
                    chipsPerPlayer = value;
                    OnPropertyChanged("ChipsPerPlayer");
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

        private int minAmoutPlayers;
        public int MinAmoutPlayers
        {
            get { return minAmoutPlayers; }
            set
            {
                if (minAmoutPlayers != value)
                {
                    minAmoutPlayers = value;
                    OnPropertyChanged("MinAmoutPlayers");
                }
            }
        }

        private int players;
        public int Players
        {
            get { return players; }
            set
            {
                if (players != value)
                {
                    players = value;
                    OnPropertyChanged("Players");
                    OnPropertyChanged("CanJoin");
                }
            }
        }

        private int maxAmoutPlayers;
        public int MaxAmoutPlayers
        {
            get { return maxAmoutPlayers; }
            set
            {
                if (maxAmoutPlayers != value)
                {
                    maxAmoutPlayers = value;
                    OnPropertyChanged("MaxAmoutPlayers");
                    OnPropertyChanged("CanJoin");
                }
            }
        }

        private bool isSpectAllow;
        public bool IsSpectAllow
        {
            get { return isSpectAllow; }
            set
            {
                if (isSpectAllow != value)
                {
                    isSpectAllow = value;
                    OnPropertyChanged("IsSpectAllow");
                }
            }
        }


        public bool CanJoin { get { return Players < MaxAmoutPlayers; } }


        public Game()
        {
        }

        public Game(GamePolicy gp,
                    int buyIn,
                    int chipsPerPlayer,
                    int minBet,
                    int minAmoutPlayers,
                    int maxAmoutPlayers,
                    bool isSpectAllow)
        {
            this.gp = gp;
            this.buyIn = buyIn;
            this.players = 0;
            this.chipsPerPlayer = chipsPerPlayer;
            this.minBet = minBet;
            this.minAmoutPlayers = minAmoutPlayers;
            this.maxAmoutPlayers = maxAmoutPlayers;
            this.isSpectAllow = isSpectAllow;
        }

    }

}
