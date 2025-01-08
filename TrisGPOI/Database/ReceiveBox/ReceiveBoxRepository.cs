using Microsoft.EntityFrameworkCore;
using TrisGPOI.Core.ReceiveBox.Entities;
using TrisGPOI.Core.ReceiveBox.Interfaces;
using TrisGPOI.Database.Context;

namespace TrisGPOI.Database.ReceiveBox
{
    public class ReceiveBoxRepository : IReceiveBoxRepository
    {
        private readonly IDbContextFactory _dbContextFactory;
        public ReceiveBoxRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<List<DBReceiveBox>> GetReceiveBox(string email)
        {
            await using var context = _dbContextFactory.CreateMySQLDbContext();
            return await context.ReceiveBox.Where(rb => rb.Receiver == email).ToListAsync();
        }
        public async Task SendReceiveBox(string sender, string receiver, string title, string message)
        {
            await using var context = _dbContextFactory.CreateMySQLDbContext();
            await context.ReceiveBox.AddAsync(new DBReceiveBox
            {
                Sender = sender,
                Receiver = receiver,
                Title = title,
                Message = message,
                Date = DateTime.UtcNow,
                ExpireDate = DateTime.UtcNow.AddDays(30),
                IsRead = false
            });
            await context.SaveChangesAsync();
        }
        public async Task DeleteReceiveBox(int Id)
        {
            await using var context = _dbContextFactory.CreateMySQLDbContext();
            var receiveBox = await context.ReceiveBox.FirstOrDefaultAsync(rb => rb.Id == Id);
            if (receiveBox != null)
            {
                context.ReceiveBox.Remove(receiveBox);
                await context.SaveChangesAsync();
            }
        }
        public async Task ReadReceiveBox(int Id)
        {
            await using var context = _dbContextFactory.CreateMySQLDbContext();
            var receiveBox = await context.ReceiveBox.FirstOrDefaultAsync(rb => rb.Id == Id);
            if (receiveBox != null)
            {
                receiveBox.IsRead = true;
                await context.SaveChangesAsync();
            }
        }
        public async Task<bool> ExistReceiveBox(int Id)
        {
            await using var context = _dbContextFactory.CreateMySQLDbContext();
            return await context.ReceiveBox.AnyAsync(rb => rb.Id == Id);
        }
        public async Task<bool> ExistUnreadMailBox(string email)
        {
            await using var context = _dbContextFactory.CreateMySQLDbContext();
            return await context.ReceiveBox.AnyAsync(rb => rb.Receiver == email && !rb.IsRead);
        }
        public async Task RemoveExpiredReceiveBox(string email)
        {
            await using var context = _dbContextFactory.CreateMySQLDbContext();
            var receiveBox = await context.ReceiveBox.Where(rb => rb.Receiver == email && rb.ExpireDate < DateTime.UtcNow).ToListAsync();
            context.ReceiveBox.RemoveRange(receiveBox);
            await context.SaveChangesAsync();
        }
        public async Task MarkAsUnread(int Id)
        {
            await using var context = _dbContextFactory.CreateMySQLDbContext();
            var receiveBox = await context.ReceiveBox.FirstOrDefaultAsync(rb => rb.Id == Id);
            if (receiveBox != null)
            {
                receiveBox.IsRead = false;
                await context.SaveChangesAsync();
            }
        }
        public async Task<bool> VerifyReceiveBox(int Id, string email)
        {
            await using var context = _dbContextFactory.CreateMySQLDbContext();
            return await context.ReceiveBox.AnyAsync(rb => rb.Id == Id && rb.Receiver == email);
        }
    }
}

