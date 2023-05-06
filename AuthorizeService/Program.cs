using AuthorizeService.Data;
using AuthorizeService.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add DB Context
if (builder.Environment.IsProduction())
{
    Console.WriteLine("--> Production Environment! Using SQL Server");
    builder.Services.AddDbContext<AppDbContext>(
        opt => opt.UseSqlServer(
            builder.Configuration.GetConnectionString("AutorizeConn")
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
// Dependency inyection.
// Repositories
builder.Services.AddScoped<IPermitionRepo, PermitionRepo>();
builder.Services.AddScoped<IPermitionRolRepo, PermitionRolRepo>();
builder.Services.AddScoped<IRolRepo, RolRepo>();

// IMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Add services to the container.
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

app.Run();
