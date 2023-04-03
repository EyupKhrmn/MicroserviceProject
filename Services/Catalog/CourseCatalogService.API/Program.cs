using CourseCatalogService.API.Model;
using CourseCatalogService.API.Services;
using CourseCatalogService.API.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

DatabaseSettings databaseSettings;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});

builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IDatabaseSettings, DatabaseSettings>();

//hata burada course serviste herhangi bir iþlem yapýnca hata veriyor
builder.Services.AddScoped<IMongoCollection<Category>>(s =>
        s.GetService<IMongoClient>()!.GetDatabase("catalogDb").GetCollection<Category>("categories"));

builder.Services.AddScoped<IMongoCollection<Course>>(s =>
         s.GetService<IMongoClient>()!.GetDatabase("catalogDb").GetCollection<Course>("Courses"));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
