﻿using TrisGPOI.Database.Game.Entities;

namespace TrisGPOI.Core.Game.Interfaces
{
    public interface IGameRepository
    {
        Task<DBGame?> SearchPlayerPlayingOrWaitingGame(string email);
        Task<bool> JoinSomeGame(string typeGame, string emailPlayer2);
        Task StartJoinGame(string typeGame, string emailPlayer1, string emptyBoard);
        Task<DBGame?> UpdateBoard(string email, string board);
        Task<string> GameFinished(int id, char winning);
        Task CancelSearchGame(string email);
        Task<DBGame?> SearchPlayerPlayingGame(string email);
        Task StartJoinCPUGame(string typeGame, string emailPlayer1, string Difficult, string emptyBoard);
        Task<DBGame?> FindGameWithId(int id);
        Task<DBGame?> GetLastGame(string email);
        Task FriendGame(string emailPlayer1, string emailPlayer2, string gameType, string emptyBoard);
    }
}
