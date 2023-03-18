using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicTacToe.Domain.Entities;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Infrastructure.EntityTypeConfigurations
{
	public class GameEntityTypeConfiguration : IEntityTypeConfiguration<Game>
	{
		public void Configure(EntityTypeBuilder<Game> builder)
		{
			builder.HasKey(k => k.Id);

			builder
				.Property(p => p.Status)
				.HasDefaultValue(GameStatus.Planned)
				.IsRequired()
				.HasMaxLength(127);

			builder
				.Property(p => p.PlayerTurn)
				.IsRequired();

			builder
				.Property(p => p.CurrentState)
				.HasDefaultValue(new[] { "...", "...", "..." });

			builder.HasOne(x => x.BluePlayer).WithMany(x => x.GamesLikeBluePlayer);
			builder.HasOne(x => x.RedPlayer).WithMany(x => x.GamesLikeRedPlayer);
		}
	}
}
