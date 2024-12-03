using HotChocolate;
using Microsoft.EntityFrameworkCore;
using Back_End.Model.Crust_db;
using Back_End.Model;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Back_End;
using Back_End.Request_Handler;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

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

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddSubscriptionType<Subscription>()
    .AddMutationType<Mutation>()
    .AddMutationConventions()
    .AddInMemorySubscriptions()
    .AddFiltering();

builder.Services.AddDbContext<CrustDb_Context>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("Crust_DBConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Crust_DBConnection")));
});

builder.Services.AddControllers();
builder.Services.AddScoped<Crust_Service>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try{
        var context = scope.ServiceProvider.GetRequiredService<CrustDb_Context>();
        context.Database.Migrate();
        RelationalDatabaseCreator? databaseCreator = context.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
        await databaseCreator!.CreateAsync();
    }
    catch (Exception e){
        Console.WriteLine(e.Data);
    }
}

app.UseWebSockets();
app.UseRouting();
app.MapGraphQL();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapControllers();
app.MapGraphQLWebSocket();
app.UseCors("AllowSpecificOrigin");
app.Run();
