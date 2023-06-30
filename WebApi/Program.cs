using System.Runtime.CompilerServices;
using WebApi.Repository;
using WebApi.Services;
using WebApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AddServices(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

SeedData(app);

app.Run();


void AddServices(WebApplicationBuilder builder)
{
    builder.Services.AddSingleton<IntGenerator>();
    builder.Services.AddSingleton<GenericRepository<Movie>>();
    builder.Services.AddTransient<MovieService>();
}


void SeedData(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var provider = scope.ServiceProvider;
        var movieRepo = provider.GetService<GenericRepository<Movie>>();
        movieRepo.Add(new Movie() { Director = new Director() { Name = "Bob", SureName = "Nowak" }, Title = "Shrek", Date = DateTime.Now - TimeSpan.FromDays(80) });
        movieRepo.Add(new Movie() { Director = new Director() { Name = "Bob", SureName = "Nowak" }, Title = "Shrek2", Date = DateTime.Now - TimeSpan.FromDays(30) });
        movieRepo.Add(new Movie() { Director = new Director() { Name = "Adam", SureName = "Kowal" }, Title = "Interplanet", Date = DateTime.Now - TimeSpan.FromDays(120) });
        movieRepo.Add(new Movie() { Director = new Director() { Name = "Mati", SureName = "Fish" }, Title = "Kot w butach", Date = DateTime.Now - TimeSpan.FromDays(70) });
        movieRepo.Add(new Movie() { Director = new Director() { Name = "Franek", SureName = "Spoon" }, Title = "Fast and lazy", Date = DateTime.Now - TimeSpan.FromDays(30) });
        movieRepo.Add(new Movie() { Director = new Director() { Name = "Karol", SureName = "Fork" }, Title = "Kebab", Date = DateTime.Now - TimeSpan.FromDays(2) });
        movieRepo.Add(new Movie() { Director = new Director() { Name = "Adam", SureName = "Sofa" }, Title = "It", Date = DateTime.Now - TimeSpan.FromDays(10) });
        movieRepo.Add(new Movie() { Director = new Director() { Name = "Henry", SureName = "Kowalski" }, Title = "Naruto", Date = DateTime.Now - TimeSpan.FromDays(25) });
        movieRepo.Add(new Movie() { Director = new Director() { Name = "Bob", SureName = "Nowak" }, Title = "Demon slayer", Date = DateTime.Now - TimeSpan.FromDays(1) });
    }
}
