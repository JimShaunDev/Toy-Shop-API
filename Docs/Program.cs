using Microsoft.EntityFrameworkCore;
using ToyShopAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//add data source
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlite("Data Source=./Data/AppDb.db"));

//add swashbuckle for visual api features
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//add swashbuckle to program start
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
