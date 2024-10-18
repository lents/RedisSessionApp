using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using RedisSessionApp.Services;

namespace RedisSessionApp.Pages.MongoDocuments
{
    public class CreateModel : PageModel
    {
        private readonly MongoService _mongoService;

        public CreateModel(MongoService mongoService)
        {
            _mongoService = mongoService;
        }

        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public int Age { get; set; }

        public async Task<IActionResult> OnPost()
        {
            var newDocument = new BsonDocument
        {
            { "name", Name },
            { "email", Email },
            { "age", Age }
        };

            await _mongoService.AddDocumentAsync(newDocument);
            return RedirectToPage("./Index");
        }
    }

}
