using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicTacToe.Domain.Entities;

namespace TicTacToe.Infrastructure.EntityTypeConfigurations
{
	public class PlayerEntityTypeConfiguration : IEntityTypeConfiguration<Player>
	{
		public void Configure(EntityTypeBuilder<Player> builder)
		{
			builder.HasKey(k => k.Id);

			builder.Property(p => p.Name).IsRequired().HasMaxLength(31);
		}
	}
}
