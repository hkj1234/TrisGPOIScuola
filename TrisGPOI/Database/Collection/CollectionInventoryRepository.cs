using Microsoft.EntityFrameworkCore;
using TrisGPOI.Core.Collection.Interfaces;
using TrisGPOI.Database.Collection.Entities;

namespace TrisGPOI.Database.Collection
{
    public class CollectionInventoryRepository : ICollectionInventoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CollectionInventoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DBCollectionInventory>> GetInventory(string userEmail)
        {
            return await _context.CollectionInventory.Where(i => i.Email == userEmail).ToListAsync();
        }

        public async Task addCollection(string userEmail, int collectionId, int quantity)
        {
            var collection = await _context.CollectionInventory.FirstOrDefaultAsync(c => c.Email == userEmail && c.CollectionID == collectionId);
            if (collection == null)
            {
                await _context.CollectionInventory.AddAsync(new DBCollectionInventory 
                { 
                    Email = userEmail, 
                    CollectionID = collectionId,
                    Quantity = quantity
                });
            }
            else
            {
                collection.Quantity += quantity;
            }
            await _context.SaveChangesAsync();
        }
        public async Task removeCollection(string userEmail, int collectionId, int quantity)
        {
            var collection = await _context.CollectionInventory.FirstOrDefaultAsync(c => c.Email == userEmail && c.CollectionID == collectionId);
            if (collection != null)
            {
                collection.Quantity -= quantity;
                if (collection.Quantity <= 0)
                {
                    _context.CollectionInventory.Remove(collection);
                }
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> anyCollection(string userEmail, int collectionId)
        {
            return await _context.CollectionInventory.AnyAsync(c => c.Email == userEmail && c.CollectionID == collectionId);
        }
    }
}
