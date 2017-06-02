using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldemClient.Bridge;
using TexasHoldemClient.Models;

namespace TexasHoldemClient.bl
{
    abstract class ServerAPI
    {
        public static ServerAPI serverApi { get { return api; } }
        private static ServerAPI api = new ConcreteImplementor();

        
        public abstract Task<bool> Register(string username, string email, string password);
        public abstract Task<User> Login(string username, string password);

        public abstract Task<Game> createNewGame(GamePolicy gp,
                                                int buyIn,
                                                int chipsPerPlayer,
                                                int minBet,
                                                int minAmoutPlayers,
                                                int maxAmoutPlayers,
                                                bool isSpectAllow);
        public abstract Task<ICollection<Game>> getActiveGames();

    }
}
