using Microsoft.EntityFrameworkCore;
using MovementService.AsyncDataService;
using MovementService.Data;
using MovementService.EventProcessing;
using MovementService.Repos;
using MovementService.SyncDataService.Grpc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// if (builder.Environment.IsProduction())
// {
Console.WriteLine("--> Using SQL Server Database, Production Environment");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(
    builder.Configuration.GetConnectionString("MovementsConn")
));
// }
// else
// {
//     Console.WriteLine("--> Using In Memory Database, Dev Environment");
//     builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
// }

builder.Services.AddControllers();

// Dependency Injection
builder.Services.AddScoped<IMovementRepo, MovementRepo>();
builder.Services.AddScoped<ITypeRepo, MovementTypeRepo>();
builder.Services.AddScoped<IAccountRepo, AccountRepo>();
// IMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Message Bus Client
builder.Services.AddSingleton<IEventProcessor, EventProcessor>();
// GRPC Client
builder.Services.AddSingleton<IAccountDataService, AccountDataService>();

// Listener on Message Bus
builder.Services.AddHostedService<MessageBusSubscriber>();
builder.Services.AddSingleton<IMessageBus, MessageBus>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// PrepDb.PrepPopulation(app, app.Environment.IsProduction());

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
