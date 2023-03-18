namespace TicTacToe.Contracts.Responses
{
	public class PlayerResponse
	{
		public Guid Id { get; set; }
		public string? Name { get; init; }
		public IEnumerable<GameResponse>? Games { get; set; }
	}
}
