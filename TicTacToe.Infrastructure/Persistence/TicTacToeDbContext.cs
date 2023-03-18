using Microsoft.EntityFrameworkCore;
using TicTacToe.Domain.Entities;
using TicTacToe.Infrastructure.EntityTypeConfigurations;

namespace TicTacToe.Infrastructure.Persistence
{
	public class TicTacToeDbContext : DbContext
	{
		public DbSet<Player> Players { get; set; }
		public DbSet<Game> Games { get; set; }

		public TicTacToeDbContext(DbContextOptions<TicTacToeDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameEntityTypeConfiguration).Assembly);

			base.OnModelCreating(modelBuilder);
		}
	}
}
