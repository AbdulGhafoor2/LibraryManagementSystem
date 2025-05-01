

using Library_Management_System.DataAccessLayer.Interfaces;
using Library_Management_System.DataAccessLayer.Repository;
using Library_Management_System.Helpers;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddScoped<IDbConnection>(db =>
    new MySqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton<DbConnectionFactory>();

// For per request
//builder.Services.AddScoped<IDbConnection>(provider =>
//{
//    var configuration = provider.GetRequiredService<IConfiguration>();
//    var connectionString = configuration.GetConnectionString("DefaultConnection");
//    return new MySqlConnection(connectionString);
//});

builder.Services.AddScoped<IBookRepository, BookRepository>();
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

app.Run();
