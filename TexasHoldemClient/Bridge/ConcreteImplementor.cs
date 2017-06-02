using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldemClient.bl;
using TexasHoldemClient.Exceptions;
using TexasHoldemClient.Models;

namespace TexasHoldemClient.Bridge
{
    class ConcreteImplementor : ServerAPI
    {
        private LinkedList<User> registeredUsers = new LinkedList<User>();
        private LinkedList<Game> activeGames = new LinkedList<Game>();

        public async override Task<bool> Register(string username, string email, string password)
        {
            int wallet = 100;
            int league = 5;
            registeredUsers.AddFirst(new User(username, email, password, wallet, league));
            return true;
        }

        public async override Task<User> Login(string username, string password)
        {


            foreach (User u in registeredUsers)
            {
                //Console.WriteLine("username: {0}, password: {1}", u.Username , u.Password);
                if (u.Username == username && u.Password == password)
                {
                    return u;
                }
            }
            throw new LoginException();
        }

        public async override Task<Game> createNewGame(GamePolicy gp,
                                              int buyIn,
                                              int chipsPerPlayer,
                                              int minBet,
                                              int minAmoutPlayers,
                                              int maxAmoutPlayers,
                                              bool isSpectAllow)
        {


            Game g = new Game(gp, buyIn, chipsPerPlayer, minBet, minAmoutPlayers, maxAmoutPlayers, isSpectAllow);
            activeGames.AddFirst(g);
            return g;
        }

        public async override Task<ICollection<Game>> getActiveGames()
        {
            return activeGames;
        }
    }
}
