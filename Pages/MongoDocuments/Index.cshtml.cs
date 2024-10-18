using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;
using RedisSessionApp.Services;

namespace RedisSessionApp.Pages.MongoDocuments
{
    public class IndexModel : PageModel
    {
        private readonly MongoService _mongoService;

        public List<BsonDocument> Documents { get; set; }

        public IndexModel(MongoService mongoService)
        {
            _mongoService = mongoService;
        }

        public async Task OnGet()
        {
            var filter = Builders<BsonDocument>.Filter.Empty;
            Documents = await _mongoService.GetDocumentsAsync(filter);
        }
    }

}
