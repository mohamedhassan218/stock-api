using API.Data; // Imports the Data namespace from the API project, likely containing the ApplicationDbContext class.
using API.Interfaces; // Imports the Interfaces namespace from the API project, likely containing the repository interfaces.
using API.Repositories; // Imports the Repositories namespace from the API project, likely containing the repository implementations.
using Microsoft.EntityFrameworkCore; // Imports the Entity Framework Core namespace, which provides functionality for working with databases.

// Create a builder for the web application.
var builder = WebApplication.CreateBuilder(args);

// Add our controllers to the services collection, enabling MVC pattern.
builder.Services.AddControllers();

// Add services for API endpoint exploration and Swagger generation.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

// Connect with our database by adding the ApplicationDbContext to the services collection.
// Use SQL Server as the database provider and configure it with the connection string from the configuration.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Register repository services with scoped lifetimes, meaning a new instance is created per HTTP request.
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

// Build the application.
var app = builder.Build();

// Configure the HTTP request pipeline.
// If the application is in development environment, enable Swagger and Swagger UI for API documentation.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enforce HTTPS redirection to ensure secure communication.
app.UseHttpsRedirection();

// Map the application's request pipeline to the controllers.
app.MapControllers();

// Run the application.
app.Run();
