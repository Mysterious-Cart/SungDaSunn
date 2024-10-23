using HotChocolate;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGraphQL();
builder.Services.AddDbContext<Model.Crust_db>();
builder.Services.AddControllers();

var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.Run();
