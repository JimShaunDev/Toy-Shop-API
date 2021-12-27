using Microsoft.EntityFrameworkCore;
using ToyShopAPI.Classes;
using ToyShopAPI.Data;
using ToyShopAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//add data source
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlite("Data Source=./Data/AppDb.db"));


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

app.UseAuthorization();

app.MapControllers();

//add swashbuckle to program start
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
