using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TicTacToe.Domain.Entities;
using TicTacToe.Domain.Extensions;
using TicTacToe.Infrastructure.Persistence;

namespace TicTacToe.Infrastructure.Extensions
{
	public static class ServiceProviderExtensions
	{
		public static async Task SeedAsync(this IServiceProvider serviceProvider)
		{
			await using (var scope = serviceProvider.CreateAsyncScope())
			{
				var dbContext = scope.ServiceProvider.GetRequiredService<TicTacToeDbContext>();

				if (dbContext.Database.IsNpgsql())
				{
					await dbContext.Database.MigrateAsync();
				}

				if (!await dbContext.Games.AnyAsync() && !await dbContext.Players.AnyAsync())
				{
					var players = new List<Player>
					{
						new()
						{
							Id = Guid.NewGuid(),
							Name = "John",
						},
						new()
						{
							Id = Guid.NewGuid(),
							Name = "Vlad",
						},
						new()
						{
							Id = Guid.NewGuid(),
							Name = "Bella",
						},
						new()
						{
							Id = Guid.NewGuid(),
							Name = "Rose"
						}
					};

					var games = new List<Game>()
					{
						new()
						{
							BluePlayerId = players[0].Id,
							RedPlayerId = players[1].Id,
							PlayerTurn = players[0].Id,
						},
						new()
						{
							BluePlayerId = players[3].Id,
							RedPlayerId = players[2].Id,
							PlayerTurn = players[2].Id,
						}
					};

					players[0].GamesLikeBluePlayer.InitIfNullAndAdd(games[0]);
					players[1].GamesLikeRedPlayer.InitIfNullAndAdd(games[0]);
					players[2].GamesLikeRedPlayer.InitIfNullAndAdd(games[1]);
					players[3].GamesLikeBluePlayer.InitIfNullAndAdd(games[1]);

					await dbContext.Players.AddRangeAsync(players);
					await dbContext.Games.AddRangeAsync(games);
					await dbContext.SaveChangesAsync();
				}
			}
		}
	}
}
