namespace TicTacToe.Contracts.Responses
{
	public class GameResponse
	{
		public Guid Id { get; set; }
		public Guid PlayerTurn { get; set; }
		public string[]? CurrentState { get; set; }
		public Guid RedPlayerId { get; set; }
		public PlayerResponse? RedPlayer { get; set; }
		public Guid BluePlayerId { get; set; }
		public PlayerResponse? BluePlayer { get; set; }
		public string? Status { get; set; }
		public int StepCount { get; set; }
	}
}
