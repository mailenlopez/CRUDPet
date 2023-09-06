using Application;
using Infrastructure;
using System.Data.SQLite;

//string connectionString = "Data Source=mydatabase.db;Version=3;";

//// Crea la base de datos si no existe
//using (SQLiteConnection connection = new SQLiteConnection(connectionString))
//{
//    string query = @"
//    ATTACH DATABASE 'BLAPet.db' AS BLAPet;
//    CREATE TABLE BLAPet.[Pet]
//    (
//        Id UUID PRIMARY KEY,
//        Name TEXT NOT NULL,
//        Description TEXT NOT NULL,
//        Lineage TEXT,
//        CreatedBy TEXT,
//        CreatedDate DATETIME,
//        LastModifiedBy TEXT,
//        LastModifiedDate DATETIME
//    );

//    CREATE TABLE BLAPet.[User]
//    (
//        Id UUID PRIMARY KEY,
//        Email TEXT NOT NULL,
//        PasswordHash TEXT NOT NULL,
//        Name TEXT NOT NULL,
//        CreatedBy TEXT,
//        CreatedDate DATETIME,
//        LastModifiedBy TEXT,
//        LastModifiedDate DATETIME
//    );
//    ";
//    connection.Open();

//    using(SQLiteCommand cmd = new SQLiteCommand(query, connection))
//    {
//        cmd.ExecuteNonQuery();
//    }

//}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.ConfigurePersistence(builder.Configuration);

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
