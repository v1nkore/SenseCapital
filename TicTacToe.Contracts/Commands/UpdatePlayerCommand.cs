namespace TicTacToe.Contracts.Commands
{
	public record UpdatePlayerCommand
	{
		public Guid Id { get; init; }
		public string? Name { get; init; }
	}
}
