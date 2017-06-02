using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldemClient.Models
{
    public enum GamePolicy
    {
        Limit,
        NOLimit,
        PotLimit
    }

    class Game
    {
        private GamePolicy gp;
        private int buyIn;
        private int chipsPerPlayer;
        private int minBet;
        private int minAmoutPlayers;
        private int maxAmoutPlayers;
        private bool isSpectAllow;

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
            this.chipsPerPlayer = chipsPerPlayer;
            this.minBet = minBet;
            this.minAmoutPlayers = minAmoutPlayers;
            this.maxAmoutPlayers = maxAmoutPlayers;
            this.isSpectAllow = isSpectAllow;
        }

    }

}
