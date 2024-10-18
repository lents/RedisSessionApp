using RedisSessionApp.Services;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add Redis configuration for session caching
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "SampleInstance";  // Optional, used to namespace Redis keys
});
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost:6380")); // or your Redis connection string


// Add Session support
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Session timeout
    options.Cookie.HttpOnly = true;  // Makes the session cookie inaccessible from JavaScript
    options.Cookie.IsEssential = true;  // Ensures cookie is sent with every request
});
builder.Services.AddSingleton<MongoService>();
var app = builder.Build();

// Use Session Middleware
app.UseSession();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});
app.Run();
