using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Contracts.Commands
{
	public class DoStepCommand
	{
		public Guid GameId { get; set; }
		public Guid PlayerId { get; set; }
		[Range(1, 9)]
		public int Cell { get; set; }
	}
}
