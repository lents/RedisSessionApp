using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using RedisSessionApp.Services;
using StackExchange.Redis;

namespace RedisSessionApp.Pages.MongoDocuments
{
    public class DetailsModel : PageModel
    {
        private readonly MongoService _mongoService;
        private readonly IDatabase _redisDb;

        public BsonDocument Document { get; set; }

        public DetailsModel(MongoService mongoService, IConnectionMultiplexer redis)
        {
            _mongoService = mongoService;
            _redisDb = redis.GetDatabase();
        }

        public async Task<IActionResult> OnGet(string id)
        {
            string redisKey = $"mongo:data:{id}";

            // Try getting data from Redis
            var cachedData = await _redisDb.StringGetAsync(redisKey);
            if (cachedData.HasValue)
            {
                Document = BsonDocument.Parse(cachedData);
                return Page();
            }

            // If not found in Redis, get from MongoDB
            Document = await _mongoService.GetDocumentAsync(id);
            if (Document == null)
            {
                return NotFound();
            }

            // Cache in Redis
            await _redisDb.StringSetAsync(redisKey, Document.ToJson(), TimeSpan.FromMinutes(5));
            return Page();
        }
    }

}
