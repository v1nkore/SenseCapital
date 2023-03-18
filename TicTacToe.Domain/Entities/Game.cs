using TicTacToe.Domain.Enums;

namespace TicTacToe.Domain.Entities
{
	public class Game : EntityBase
	{
		public string[] CurrentState { get; set; } = null!;
		public Guid BluePlayerId { get; set; }
		public Player? BluePlayer { get; set; }
		public Guid RedPlayerId { get; set; }
		public Player? RedPlayer { get; set; }
		public GameStatus Status { get; set; }
		public int StepCount { get; set; }
		public Guid? PlayerTurn { get; set; }

		public bool CheckWin()
		{
			const char x = 'X';
			if (CurrentState[0][0] == x && CurrentState[0][1] == x && CurrentState[0][2] == x
				|| CurrentState[1][0] == x && CurrentState[1][1] == x && CurrentState[1][2] == x
				|| CurrentState[2][0] == x && CurrentState[2][1] == x && CurrentState[2][2] == x)
			{
				return true;
			}

			if (CurrentState[0][0] == x && CurrentState[1][0] == x && CurrentState[2][0] == x
					 || CurrentState[0][1] == x && CurrentState[1][1] == x && CurrentState[2][1] == x
					 || CurrentState[0][2] == x && CurrentState[1][2] == x && CurrentState[2][2] == x)
			{
				return true;
			}

			if (CurrentState[0][0] == x && CurrentState[1][1] == x && CurrentState[2][2] == x
				|| CurrentState[0][2] == x && CurrentState[1][1] == x && CurrentState[2][0] == x)
			{
				return true;
			}

			return false;
		}
	}
}
