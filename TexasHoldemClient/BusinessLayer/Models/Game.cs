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

    public class Game : Changing
    {
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

        private string id;
        public string ID
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("ID");
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

        private int spectatingAllowed;
        public int SpectatingAllowed
        {
            get { return spectatingAllowed; }
            set
            {
                if (spectatingAllowed != value)
                {
                    spectatingAllowed = value;
                    OnPropertyChanged("SpectatingAllowed");
                }
            }
        }

    }
}
