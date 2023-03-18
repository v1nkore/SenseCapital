namespace TicTacToe.Domain.Entities
{
	public class Player : EntityBase
	{
		public string? Name { get; set; }

		public ICollection<Game>? GamesLikeBluePlayer { get; set; } = new List<Game>();
		public ICollection<Game>? GamesLikeRedPlayer { get; set; } = new List<Game>();

		public IEnumerable<Game>? GetGames()
		{
			if (GamesLikeBluePlayer is not null && GamesLikeRedPlayer is not null
				&& GamesLikeBluePlayer.Count > 0 && GamesLikeRedPlayer.Count > 0)
			{
				return GamesLikeBluePlayer.Concat(GamesLikeRedPlayer);
			}
			if (GamesLikeBluePlayer is not null && GamesLikeBluePlayer.Count > 0)
			{
				return GamesLikeBluePlayer;
			}

			if (GamesLikeRedPlayer is not null && GamesLikeRedPlayer.Count > 0)
			{
				return GamesLikeRedPlayer;
			}

			return Enumerable.Empty<Game>();
		}
	}
}
