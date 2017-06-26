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
            [Header("email")] string email,
            [Header("username")] string username,
            [Header("password")] string password,
            [Header("token")] string token);

        [Get("/createGame?gameType={gameType}&gameName={gameName}&buyin={buyin}&initialChips={initialChips}&minBet={minBet}&minPlayers={minPlayers}&maxPlayers={maxPlayers}&spectatingAllowed={spectatingAllowed}")]
        Task<dynamic> CreateGame(
            [Header("token")] string token,

            string gameName,
            int gameType,
            int buyin,
            int initialChips,
            int minBet,
            int minPlayers,
            int maxPlayers,
            bool spectatingAllowed);

        [Get("/joinGame?gameId={gameId}")]
        Task JoinGame(
            [Header("token")] string token,

            int gameId);

        
        [Get("/spectateGame?gameId={gameId}")]
        Task SpectateGame(
            [Header("token")] string token,

            int gameId);

        [Get("/leaveGame?gameId={gameId}")]
        Task LeaveGame(
            [Header("token")] string token,

            int gameId);

        [Get("/playerAction?playerId={playerId}&gameId={gameId}&newStatus={newStatus}&newBet={newBet}&")]
        Task PlayerAction(
            [Header("token")] string token,

            int playerId,
            int gameId,
            int newStatus,
            int newBet);

        [Get("/startRound?gameId={gameId}")]
        Task StartRound(
            [Header("token")] string token,

            int gameId);

        [Get("/startRound?newPassword={newPassword}")]
        Task ChangePassword(
            [Header("token")] string token,

            string newPassword);

        [Get("/startRound?newEmail={newEmail}")]
        Task ChangeEmail(
            [Header("token")] string token,

            string newEmail);
    }
}
