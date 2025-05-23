﻿using TrisGPOI.Database.ReceiveBox.Entities;

namespace TrisGPOI.Core.ReceiveBox.Interfaces
{
    public interface IReceiveBoxManager
    {
        Task<List<DBReceiveBox>> GetReceiveBox(string email);
        Task SendReceiveBox(string sender, string receiver, string title, string message);
        Task DeleteReceiveBox(int Id);
        Task ReadReceiveBox(int Id);
        Task<bool> ExistReceiveBox(int Id);
        Task<bool> ExistUnreadMailBox(string email);
        Task MarkAsUnread(int Id, string email);
    }
}


