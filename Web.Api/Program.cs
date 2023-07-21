using Application;
using Application.Interfaces;
using Domain.Entities;
using Infraestructure;
using Infraestructure.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Web.Api"))
);
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ApplicationDbContext>();

    // Apply database migration and create/update the database
    dbContext.Database.EnsureCreated();

    var userRepository = services.GetRequiredService<IUserRepository>();

    if (userRepository.IsUserTableEmpty())
    {
        var defaultUser = new User
        {
            Name = "Admin User",
            Password = "admin",
            Email = "admin@mail.com",
            Type = "admin"
        };
        userRepository.CreateUserAsync(defaultUser).Wait();
    }
}

app.Run();
