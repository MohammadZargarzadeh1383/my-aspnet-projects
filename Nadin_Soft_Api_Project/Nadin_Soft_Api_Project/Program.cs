using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Nadin_Soft_Api_Project.Application.Interfaces.ApplicationDbContext;
using Nadin_Soft_Api_Project.Application.Interfaces.Repositories;
using Nadin_Soft_Api_Project.Application.Interfaces.Services;
using Nadin_Soft_Api_Project.Domain.Entities.Product;
using Nadin_Soft_Api_Project.Domain.Entities.User;
using Nadin_Soft_Api_Project.Infrastructure.Repositories;
using Nadin_Soft_Api_Project.Infrastucture.ApplicationDb;
using Nadin_Soft_Api_Project.Infrastucture.Repositories;
using System.Text;
using WebApplication2.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Product",
        Description = "Make your Product"

    });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Please Enter \toke\n"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }

                },
             new string[] {}
        }
    });
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
builder.Services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
builder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IJwtOperation, JwtOperation>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));


#region JWTBreare
var Configuration = builder.Configuration;
var JwtSetting = Configuration.GetSection("JwtSettings");
var SecretKey = JwtSetting.GetValue<string>("SecretKey");
var key = UTF8Encoding.UTF8.GetBytes(SecretKey);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(JwtBearerOptions =>
{
    JwtBearerOptions.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var TokenFromHeader = context.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");

            var TokenFromCookie = context.Request.Cookies["Access_Token"];

            context.Token = TokenFromHeader ?? TokenFromCookie;

            return Task.CompletedTask;
        }
    };
    JwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidIssuer = JwtSetting.GetValue<string>("Issuer"),
        ValidAudience = JwtSetting.GetValue<string>("Audience"),
        ClockSkew = TimeSpan.FromMinutes(0),
    };
});
#endregion




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


app.Run();

