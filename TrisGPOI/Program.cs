using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TrisGPOI.Database.Context;
using TrisGPOI.Controllers.User;
using TrisGPOI.Controllers.Tris;
using Microsoft.AspNetCore.SignalR;
using TrisGPOI.Core.JWT.Entities;
using TrisGPOI.Hubs.TrisGameHub.Game;
using TrisGPOI.Hubs.HomeHub;

var builder = WebApplication.CreateBuilder(args);

//for test
//builder.WebHost.UseUrls("http://0.0.0.0:8080");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//database SQL server
/*
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
*/

//database MySQL 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(5, 5, 62))
    )
);

// Configura JWT
// Questo serve per contorllare se JWT ?valido o no rispettando le seguente regole 
#pragma warning disable 8602, 8604
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
var key = Encoding.ASCII.GetBytes(tokenOptions.Secret);
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(key),
        };

        // Abilita il supporto per SignalR
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];

                // Controlla che sia una richiesta SignalR
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/gameHub"))
                {
                    context.Token = accessToken;
                }

                return Task.CompletedTask;
            }
        };
    });
#pragma warning restore 8602, 8604

//per mandare la notifica dal server al client
builder.Services.AddSignalR();

//DE
builder.Services
    .AddCustomer()
    .AddTrisNormale()
    .AddContext();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHub<TrisNormaleHub>("Normal"); // Mappa l'hub SignalR
app.MapHub<TrisNormaleCPUHub>("NormalCPU");
app.MapHub<TrisInfinityHub>("Infinity");
app.MapHub<TrisInfinityCPUHub>("InfinityCPU");

app.MapHub<HomeHub>("Home");

app.Run();