using Microsoft.AspNetCore.Mvc.RazorPages;
using StackExchange.Redis;

public class IndexModel : PageModel
{
    public void OnGet()
    {
        try
        {
            // Retrieve session data
            var userName = HttpContext.Session.GetString("UserName");

            if (string.IsNullOrEmpty(userName))
            {
                userName = "Guest";
            }

            ViewData["Message"] = $"Hello, {userName}!";
        }
        catch (RedisConnectionException ex)
        {
            // Handle Redis connection issues
            ViewData["Message"] = "Redis server is currently unavailable. Please try again later.";
            // Log the exception
        }
    }

    public void OnPost(string userName)
    {
        try
        {
            // Set session data (stored in Redis)
            HttpContext.Session.SetString("UserName", userName);

            // Redirect to avoid form resubmission issues
            OnGet();
        }
        catch (RedisConnectionException ex)
        {
            // Handle Redis connection issues
            ViewData["Message"] = "Redis server is currently unavailable. Please try again later.";
            // Log the exception
        }
       
    }
}
