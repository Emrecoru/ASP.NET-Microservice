using InventoryService.Data.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Data.Repositories
{
    public class ItemInventoryRepository
    {
        private readonly IMongoCollection<ItemInventory> _itemInventoryCollection;
        public ItemInventoryRepository()
        {
            var client = new MongoClient("mongodb://localhost:27017/");

            var database = client.GetDatabase("InventoryDb");

            this._itemInventoryCollection = database.GetCollection<ItemInventory>("itemInventories");
        }

        public async Task<List<ItemInventory>> Get()
        {
            var filter = Builders<ItemInventory>.Filter.Empty;

            var result = await _itemInventoryCollection.Find(filter).ToListAsync();

            return result;
        }

        public async Task<ItemInventory> GetItemInventory(string itemId, string inventoryId)
        {
            var filter1 = Builders<ItemInventory>.Filter.Eq(x => x.ItemId, itemId);
            var filter2 = Builders<ItemInventory>.Filter.Eq(x => x.InventoryId, inventoryId);

            var filter = Builders<ItemInventory>.Filter.And(filter1, filter2);

            var result = await _itemInventoryCollection.Find(filter).FirstOrDefaultAsync();

            return result;
        }

        public async Task<ItemInventory> Create(ItemInventory inventory)
        {
            await _itemInventoryCollection.InsertOneAsync(inventory);

            return inventory;
        }

        public async Task Update(ItemInventory updatedInventory)
        {
            var filter = Builders<ItemInventory>.Filter.Eq(x => x.Id, updatedInventory.Id);

            await _itemInventoryCollection.FindOneAndReplaceAsync(filter, updatedInventory);
        }

        public async Task Remove(string id)
        {
            var filter = Builders<ItemInventory>.Filter.Eq(x => x.Id, id);

            await _itemInventoryCollection.FindOneAndDeleteAsync(filter);
        }
    }
}
