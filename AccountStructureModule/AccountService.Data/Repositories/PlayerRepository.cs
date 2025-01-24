

using AccountService.Data.Entities;
using MongoDB.Driver;

namespace AccountService.Data.Repositories
{
    public class PlayerRepository
    {
        private readonly IMongoCollection<Player> playerCollection;
        public PlayerRepository()
        {
            var client = new MongoClient("mongodb://localhost:27017/");

            var database = client.GetDatabase("AccountStructureDb");

            this.playerCollection = database.GetCollection<Player>("players");
        }

        public async Task<Player> Create(Player player)
        {
            await playerCollection.InsertOneAsync(player);

            return player;
        }

        public async Task<List<Player>?> GetAll()
        {
            var filter = Builders<Player>.Filter.Empty;

            var result = await playerCollection.Find(filter).ToListAsync();

            return result;
        }

        public async Task<Player> GetById(string id)
        {
            var filter = Builders<Player>.Filter.Eq(x => x.Id, id);

            var result = await playerCollection.Find(filter).FirstOrDefaultAsync();

            return result;
        }

        public async Task Update(Player updatedPlayer)
        {
            var filter = Builders<Player>.Filter.Eq(x => x.Id, updatedPlayer.Id);
            
            await playerCollection.FindOneAndReplaceAsync(filter, updatedPlayer);
        }

        public async Task Remove(string id)
        {
            var filter = Builders<Player>.Filter.Eq(x => x.Id, id);

            await playerCollection.DeleteOneAsync(filter);
        }
    }
}
