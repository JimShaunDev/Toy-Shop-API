using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToyShopAPI.Classes;
using ToyShopAPI.Data;
using ToyShopAPI.Services;
using ToyShopAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//add data source
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlite("Data Source=./Data/AppDb.db"));


builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireUppercase = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
}).AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();




//for users and JWT
var services = builder.Services;
services.AddCors();


// configure strongly typed settings object
services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// configure DI for application services
services.AddScoped<IUserService, UserService>();


//add swashbuckle for visual api features
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

//allow CORS
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//add swashbuckle to program start
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
