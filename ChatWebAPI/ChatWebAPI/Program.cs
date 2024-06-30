using ChatWeb.Api.Hubs;
using ChatWeb.Core.Interfaces;
using ChatWeb.Core.Interfaces.Repositories;
using ChatWeb.Core.Interfaces.Services;
using ChatWeb.Infrastructure.Data;
using ChatWeb.Infrastructure.Repositories;
using ChatWeb.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.AddControllers();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IMessageRepository, MessageRepository>();
builder.Services.AddTransient<IAuthenticationService, ChatWeb.Infrastructure.Services.AuthenticationService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddDbContext<ChatWebDbContext>(
    options => {
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection")
        );
    }
);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddSignalR(options => {
    options.EnableDetailedErrors = true;
});

builder.Services.AddTransient<IConnectionRepository, ConnectionRepository>();
builder.Services.AddTransient<IConnectionService, ConnectionService>();
builder.Services.AddTransient<IChatHubService, ChatHubService>();
builder.Services.AddTransient<IMessageRepository, MessageRepository>();
builder.Services.AddTransient<IMessageService, MessageService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chat");

app.Run();
