using DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Interfaces.Repositories;
using DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Interfaces.Services;
using DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Services;
using DevApp_TFF_Sortilege__WebAPI.Infrastructure.Database;
using DevApp_TFF_Sortilege__WebAPI.Infrastructure.Database.Repositories;
using DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Token;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Dependancy Injection Configuration
// - Tools
// -- TokenTools
builder.Services.AddSingleton<TokenTools>();  // Wanna keep that active for the whole connection => Singleton

// - Services (using 'AddScoped' because it seems the best compromize between Singleton and Transcient here)
builder.Services.AddScoped<IMemberService, MemberService>();

// - Repositories (idem)
builder.Services.AddScoped<IMemberRepository, MemberRepository>();

// - Mailer?

// - DB Context
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options
    .UseNpgsql(builder.Configuration.GetConnectionString("MyConnectionString")); // TODO : Question : Pq si je met en general NoTracking je n'arrive pas à mettre AsTracking sur Add ???
});


// Controllers Mapping
builder.Services.AddControllers();

// Custom Exceptions Handling?


// Authentication Configs
// - JWT
// - Auth0


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(); // TODO : Question : Qu'est-ce que ça fait en fait ???

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

// UseExceptions

// UseAuthentications

app.UseAuthorization();

app.MapControllers();

app.Run();
