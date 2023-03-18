namespace TicTacToe.Application.Constants
{
	public static class GameServiceErrors
	{
		public const string GameNotFound = "Game with the specified id was not found";
		public const string TurnError = "Now it's the other player's turn";
		public const string PlayerInGame = "Player with the specified id already in game";
		public const string CellMarked = "Cell with the specified row and column already marked";
		public const string GameEnded = "This game already ended";
		public const string SamePlayers = "Player can't plan game with himself";
	}
}
