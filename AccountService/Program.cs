using AccountService.AsyncDataServices;
using AccountService.Data;
using AccountService.EventProcesing;
using AccountService.Repositories;
using AccountService.SyncDataServices.Grpc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
if (builder.Environment.IsProduction())
{
    Console.WriteLine("--> Using SQL Server Database, Production Environment");
    builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(
        builder.Configuration.GetConnectionString("AccountsConn")
    ));
}
else
{
    Console.WriteLine("--> Using In Memory Database, Dev Environment");
    builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
}

builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<IAccountTypeRepo, AccountTypeRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<ICoinTypeRepo, CoinTypeRepo>();
// Message Bus Client
builder.Services.AddSingleton<IEventProcessor, EventProcessor>();
builder.Services.AddSingleton<IMessageBus, MessageBus>();
builder.Services.AddScoped<IUserDataClient, UserDataClient>();

builder.Services.AddHostedService<MessageBusSubscriber>();
// IMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
builder.Services.AddGrpc();

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



// app.UseHttpsRedirection();

PrepDb.PrepPopulation(app, app.Environment.IsProduction());

app.UseAuthorization();
app.MapGrpcService<GrpcAccountService>();
app.MapGet("/protos/accounts.proto", async context =>
{
    await context.Response.WriteAsync(File.ReadAllText("Protos/accounts.proto"));
});
app.MapControllers();

app.Run();
