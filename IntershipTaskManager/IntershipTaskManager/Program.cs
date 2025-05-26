using IntershipTaskManager.Application.Interfaces.ApplicationDbContext;
using IntershipTaskManager.Application.Interfaces.Repositoreis;
using IntershipTaskManager.Domain.Entities.Intership;
using IntershipTaskManager.Domain.Entities.Task;
using IntershipTaskManager.Infrastucture.Repositoreis.GenericRepository;
using IntershipTaskManager.Infrastucture.Repositoreis.IntershipRepository;
using IntershipTaskManager.Infrastucture.Repositoreis.IntershipTaskRepository;
using IntershipTaskManager.Infrastucture.Repositoreis.MissionRepository;
using IntershipTaskManager.Interfaces.ApplicationDB.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
builder.Services.AddScoped<IMissionRepository, MissionRepository>();
builder.Services.AddScoped<IIntershipRepository, IntershipRepository>();
builder.Services.AddScoped<IGenericRepository<Intership>, GenericRepository<Intership>>();
builder.Services.AddScoped<IIntershipTaskRepository, IntershipTaskRepository>();
builder.Services.AddScoped<IGenericRepository<IntershipTask>, GenericRepository<IntershipTask>>();
builder.Services.AddScoped<IMissionRepository, MissionRepository>();
builder.Services.AddScoped<IGenericRepository<Mission>, GenericRepository<Mission>>();
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(connectionString: builder.Configuration.GetConnectionString("SqlServer")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
