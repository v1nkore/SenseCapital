namespace TicTacToe.Contracts.Commands
{
	public record CreateGameCommand
	{
		public Guid BluePlayerId { get; set; }
		public Guid RedPlayerId { get; set; }
	}
}
