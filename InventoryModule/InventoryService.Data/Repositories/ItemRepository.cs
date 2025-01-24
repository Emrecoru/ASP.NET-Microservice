using InventoryService.Data.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Data.Repositories
{
    public class ItemRepository
    {
        private readonly IMongoCollection<Item> itemRepository;

        public ItemRepository(IMongoCollection<Item> itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public ItemRepository()
        {
            var client = new MongoClient("mongodb://localhost:27017/");

            var database = client.GetDatabase("InventoryDb");

            this.itemRepository = database.GetCollection<Item>("Items");
        }

        public async Task<List<Item>> GetAll()
        {
            var filter = Builders<Item>.Filter.Empty;

            var result = await itemRepository.Find(filter).ToListAsync();

            return result;
        }

        public async Task<Item?> GetById(string id)
        {
            var filter = Builders<Item>.Filter.Eq(x => x.Id, id);

            var result = await itemRepository.Find(filter).FirstOrDefaultAsync();

            return result;
        }

        public async Task<Item> Create(Item item)
        {
            await itemRepository.InsertOneAsync(item);
            
            return item;
        }

        public async Task Update(Item updatedItem)
        {
            var filter = Builders<Item>.Filter.Eq(x => x.Id, updatedItem.Id);

            await itemRepository.FindOneAndReplaceAsync(filter, updatedItem);
        }

        public async Task Remove(string id)
        {
            var filter = Builders<Item>.Filter.Eq(x => x.Id, id);

            await itemRepository.DeleteOneAsync(filter);
        }
    }
}
