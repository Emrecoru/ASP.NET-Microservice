using InventoryService.Data.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Data.Repositories
{
    public class InventoryRepository
    {
        private readonly IMongoCollection<Inventory> _inventoryCollection;
        public InventoryRepository()
        {
            var client = new MongoClient("mongodb://localhost:27017/");

            var database = client.GetDatabase("InventoryDb");

            this._inventoryCollection = database.GetCollection<Inventory>("inventories");
        }

        public async Task<List<Inventory>> Get()
        {
            var filter = Builders<Inventory>.Filter.Empty;

            var result = await _inventoryCollection.Find(filter).ToListAsync();

            return result;
        }

        public async Task<Inventory> GetById(string id)
        {
            var filter = Builders<Inventory>.Filter.Eq(x => x.Id, id);

            var result = await _inventoryCollection.Find(filter).FirstOrDefaultAsync();

            return result;
        }

        public async Task<Inventory> Create(Inventory inventory)
        {
            await _inventoryCollection.InsertOneAsync(inventory);

            return inventory;
        }

        public async Task<Inventory> Update(Inventory updatedInventory)
        {
            var filter = Builders<Inventory>.Filter.Eq(x => x.Id, updatedInventory.Id);

            await _inventoryCollection.FindOneAndReplaceAsync(filter, updatedInventory);

            return updatedInventory;
        }

        public async Task Remove(string id)
        {
            var filter = Builders<Inventory>.Filter.Eq(x => x.Id, id);

            await _inventoryCollection.FindOneAndDeleteAsync(filter);
        }
    }
}
