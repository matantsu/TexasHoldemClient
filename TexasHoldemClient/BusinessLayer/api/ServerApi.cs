using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldemClient.BusinessLayer.Models;

namespace TexasHoldemClient.BusinessLayer.api
{
    public interface ServerApi
    {
        [Get("/register")]
        Task Register(
            [Header("username")] string username, 
            [Header("password")] string password, 

            [Header("email")] string email);

        [Get("/createGame")]
        Task CreateGame(
            [Header("username")] string username, 
            [Header("password")] string password,

            [Header("gametype")] GameType gametype,
            [Header("buyin")] int buyin,
            [Header("initialChips")] int initialChips,
            [Header("minBet")] int minBet,
            [Header("minPlayers")] int minPlayers,
            [Header("maxPlayers")] int maxPlayers,
            [Header("spectatingAllowed")] bool spectatingAllowed);

        [Get("/joinGame")]
        Task JoinGame(
            [Header("username")] string username, 
            [Header("password")] string password, 

            [Header("gameId")] string gameId);

        [Get("/leaveGame")]
        Task LeaveGame(
            [Header("username")] string username, 
            [Header("password")] string password, 

            [Header("gameId")] string gameId);

        [Get("/spectateGame")]
        Task SpectateGame(
            [Header("username")] string username, 
            [Header("password")] string password, 

            [Header("gameId")] string gameId);
    }
}
