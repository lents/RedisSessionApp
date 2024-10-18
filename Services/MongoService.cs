using MongoDB.Bson;
using MongoDB.Driver;

namespace RedisSessionApp.Services
{
    public class MongoService
    {
        private readonly IMongoCollection<BsonDocument> _collection;

        public MongoService()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("MyAppDb");
            _collection = database.GetCollection<BsonDocument>("MyCollection");
        }

        // Method to fetch a document by ID
        public async Task<BsonDocument> GetDocumentAsync(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        // Method to fetch all documents
        public async Task<List<BsonDocument>> GetDocumentsAsync(FilterDefinition<BsonDocument> filter)
        {
            return await _collection.Find(filter).ToListAsync();
        }

        // Method to add a new document
        public async Task AddDocumentAsync(BsonDocument doc)
        {
            await _collection.InsertOneAsync(doc);
        }
    }

}
