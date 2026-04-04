using DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Interfaces.Repositories;
using DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Interfaces.Services;
using DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Services;
using DevApp_TFF_Sortilege__WebAPI.Infrastructure.Database;
using DevApp_TFF_Sortilege__WebAPI.Infrastructure.Database.Repositories;
using DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Configs;
using DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Dependancy Injection Configuration
// - Tools
// -- TokenTools
builder.Services.AddSingleton<TokenTools>();  // Wanna keep that active for the whole connection => Singleton

// - Services (using 'AddScoped' for Auth because it doesn't need to stay open all the time but Room does => Singleton)
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddSingleton<IRoomService, RoomService>();
//builder.Services.AddSingleton<LobbyHub>();

// - Repositories (idem)
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddSingleton<IRoomRepository, RoomRepository>();

// - Mailer?

// - WebSocket Manager
builder.Services.AddSignalR();

// - DB Context
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options
    .UseNpgsql(builder.Configuration.GetConnectionString("MyConnectionString"))
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

// - Cors Config
builder.Services.AddCors(options =>
{
    options.AddPolicy("Dev", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
    options.AddPolicy("Prod", policy =>
    {
        policy.WithOrigins("http://localhost:5173"); // TODO: Mettre variable environement genre ASP_CLIENT_URL avec l'URL du front !
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});


// Controllers Mapping
builder.Services.AddControllers();

// Custom Exceptions Handling?


// Authentication Configs
// - JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        byte[] secretKey = Encoding.UTF8.GetBytes(builder.Configuration["Token:Key"]!);

        options.TokenValidationParameters = new TokenValidationParameters()
        {

            // Config des valeurs valides 
            ValidIssuer = builder.Configuration["Token:Issuer"],
            ValidAudience = builder.Configuration["Token:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(secretKey),

            // Config de la réponse de validation attendue
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
        };
    });

// - Auth0 ?


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi( options =>
{
    // Doc for Scalar for efficiency with jwt token
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseCors("Dev");
}
else
{
    // Cors unabling
    app.UseCors("Prod");
}


app.UseHttpsRedirection();

//* UseExceptions To implement later

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHub<LobbyHub>("/lobbyhub");

app.Run();
