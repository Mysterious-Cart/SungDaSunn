using HotChocolate;
using Microsoft.EntityFrameworkCore;
using Back_End.Model.Crust_db;
using Back_End.Model;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Back_End;
using Back_End.Request_Handler;
using Microsoft.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<Crust_Service>();
builder.Services.AddPooledDbContextFactory<CrustDb_Context>(v =>
    v.UseMySql(builder.Configuration.GetConnectionString("Crust_DBConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Crust_DBConnection"))
));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((Host) => (true));

        });
});

builder.Services.AddGraphQLServer()
    .AddInMemorySubscriptions()
    .AddQueryType<Query>();

builder.Services.AddDbContext<CrustDb_Context>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("Crust_DBConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Crust_DBConnection")));
});
builder.Services.AddControllers();
builder.Services.AddScoped<Crust_Service>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<CrustDb_Context>();
        context.Database.Migrate();
        context.Database.EnsureCreated();
        var databaseCreator = (context.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator) ;
        databaseCreator.CreateTables();

    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
}

app.UseRouting();
app.MapGraphQL();
app.UseWebSockets();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapGraphQLWebSocket();
app.MapControllers();
app.UseCors("AllowSpecificOrigin");
app.Run();
