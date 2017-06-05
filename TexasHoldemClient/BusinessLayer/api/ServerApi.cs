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
        [Get("/register?email={email}")]
        Task Register(
            [Header("username")] string username, 
            [Header("password")] string password, 

            string email);

        [Get("/createGame?gameType={gameType}&buyin={buyin}&initialChips={initialChips}&minBet={minBet}&minPlayers={minPlayers}&maxPlayers={maxPlayers}&spectatingAllowed={spectatingAllowed}")]
        Task<dynamic> CreateGame(
            [Header("username")] string username, 
            [Header("password")] string password,

            int gameType,
            int buyin,
            int initialChips,
            int minBet,
            int minPlayers,
            int maxPlayers,
            bool spectatingAllowed);

        [Get("/joinGame?gameId={gameId}")]
        Task JoinGame(
            [Header("username")] string username, 
            [Header("password")] string password, 

            int gameId);

        
        [Get("/spectateGame?gameId={gameId}")]
        Task SpectateGame(
            [Header("username")] string username, 
            [Header("password")] string password, 

            int gameId);

        [Get("/leaveGame?gameId={gameId}")]
        Task LeaveGame(
            [Header("username")] string username,
            [Header("password")] string password,

            int gameId);

        [Get("/playerAction?playerId={playerId}&gameId={gameId}&newStatus={newStatus}&newBet={newBet}&")]
        Task PlayerAction(
            [Header("username")] string username,
            [Header("password")] string password,

            int playerId,
            int gameId,
            PlayerStatus newStatus,
            int? newBet);
    }
}
