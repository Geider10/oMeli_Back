using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using oMeli_Back.Context;
using oMeli_Back.Services;
using oMeli_Back.Services.Store;
using oMeli_Back.Utils;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Connection");
var secretKey = builder.Configuration.GetRequiredSection("SECRET_KEY").Value;

builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddSingleton<Util>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<SubscriptionService>();
builder.Services.AddScoped<PlanService>();
builder.Services.AddScoped<StoreService>();
builder.Services.AddScoped<ScheduleService>();

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
