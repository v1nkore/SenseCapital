using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TicTacToe.Abstractions.ServiceAbstractions;
using TicTacToe.API.Extensions;
using TicTacToe.Application.Mappers;
using TicTacToe.Application.Services;
using TicTacToe.Infrastructure.Extensions;
using TicTacToe.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();

var connectionString = string.Empty;
if (builder.Environment.IsDevelopment())
{
	connectionString = "TicTacToeLocal";
}
else if (builder.Environment.IsDeployment())
{
	connectionString = "TicTacToeDocker";
}
builder.Services.AddDbContext<TicTacToeDbContext>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString(connectionString));
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapControllers();

await app.Services.SeedAsync();

app.Run();
