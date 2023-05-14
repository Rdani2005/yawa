using AuthService.AsyncDataServices;
using AuthService.Data;
using AuthService.Repositories;
using AuthService.SyncDataServices.Grpc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DB Context
if (builder.Environment.IsProduction())
{
    Console.WriteLine("--> Production Environment! Using SQL Server");
    builder.Services.AddDbContext<AppDbContext>(
        opt => opt.UseSqlServer(
            builder.Configuration.GetConnectionString("AuthConn")
        )
    );
}
else
{
    Console.WriteLine("--> Dev Environment! Using InMemory DB");
    builder.Services.AddDbContext<AppDbContext>(
        opt => opt.UseInMemoryDatabase("InMem")
    );
}
// Add services to the container.
// Using Dependency Injection
builder.Services.AddScoped<IPermitionRepo, PermitionRepo>();
builder.Services.AddScoped<IPermitionRolRepo, PermitionRolRepo>();
builder.Services.AddScoped<IRolRepo, RolRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
// Message Bus Client
builder.Services.AddSingleton<IMessageBus, MessageBus>();
// IMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Using Grpc
builder.Services.AddGrpc();

builder.Services.AddControllers();
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

app.MapControllers();
app.MapGrpcService<GrpcUserService>();
app.MapGet("/protos/users.proto", async context =>
{
    await context.Response.WriteAsync(File.ReadAllText("Protos/users.proto"));
});
app.Run();
