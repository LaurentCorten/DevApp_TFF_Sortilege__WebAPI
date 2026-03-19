using DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Interfaces.Repositories;
using DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Interfaces.Services;
using DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Services;
using DevApp_TFF_Sortilege__WebAPI.Infrastructure.Database;
using DevApp_TFF_Sortilege__WebAPI.Infrastructure.Database.Repositories;
using DevApp_TFF_Sortilege__WebAPI.Presentation.WebAPI.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

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
    .UseNpgsql(builder.Configuration.GetConnectionString("MyConnectionString"))
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); // TODO : Mettre en NoTracking par defaut
});

// - Cors Config
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy => // TODO : Check quelle policy fait sens en prod
    {
        policy.AllowAnyOrigin();
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

// - Auth0


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// Cors unabling
app.UseCors("AllowAll");

app.UseHttpsRedirection();

//* UseExceptions To implement later

// TODO Check app.UseStaticFiles();et 
app.UseHttpsRedirection(); // TODO: Check si ça joue dans le fonctionnement des cors ou du register. Et à quoi ça sert de façon plus large
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
