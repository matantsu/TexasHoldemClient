﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;
using TexasHoldemClient.BusinessLayer.api;
using TexasHoldemClient.BusinessLayer.Models;

namespace TexasHoldemClient.BusinessLayer
{
    public class ApiManager
    {
        private ServerApi api;
        private IUserManager um;

        public ApiManager(ServerApi api, IUserManager um)
        {
            this.api = api;
            this.um = um;
        }

        public Task<dynamic> CreateGame(string gameName, int gameType, int buyin, int initialChips, int minBet, int minPlayers, int maxPlayers, bool spectatingAllowed)
        {
            return api.CreateGame(um.Token, gameName, gameType, buyin, initialChips, minBet, minPlayers, maxPlayers, spectatingAllowed);
        }

        public Task JoinGame(int gameId)
        {
            return api.JoinGame(um.Token, gameId);
        }

        public Task LeaveGame(int gameId)
        {
            return api.LeaveGame(um.Token, gameId);
        }

        public Task PlayerAction(int playerId, int gameId, PlayerStatus newStatus, int newBet)
        {
            return api.PlayerAction(um.Token, playerId, gameId, (int)newStatus, newBet);
        }

        public Task Register(string email, string username, string password, string token)
        {
            return api.Register(email, username, password, token);
        }

        public Task SpectateGame(int gameId)
        {
            return api.SpectateGame(um.Token, gameId);
        }
    }
}
