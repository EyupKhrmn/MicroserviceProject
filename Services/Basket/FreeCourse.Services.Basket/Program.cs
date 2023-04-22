using FreeCourse.Services.Basket.Services;
using FreeCourse.Services.Basket.Settings;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("RedisSettings"));
builder.Services.AddSingleton<RedisService>(sp =>
{
    var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;

    var redis = new RedisService(redisSettings.Host, redisSettings.Port);

    redis.Connect();
    return redis;
});



var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();