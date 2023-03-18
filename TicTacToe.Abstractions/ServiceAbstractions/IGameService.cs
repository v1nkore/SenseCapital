using System.Linq.Expressions;
using TicTacToe.Contracts.Commands;
using TicTacToe.Contracts.OperationResult;
using TicTacToe.Contracts.Responses;
using TicTacToe.Domain.Entities;

namespace TicTacToe.Abstractions.ServiceAbstractions
{
	public interface IGameService
	{
		Task<IEnumerable<GameResponse>> GetAllAsync(Expression<Func<Game, bool>>? filter = null);
		Task<OperationResult<GameResponse>> GetAsync(Guid id);
		Task<OperationResult<Guid>> CreateAsync(CreateGameCommand command);
		Task<OperationResult<GameResponse>> DoStepAsync(DoStepCommand command);
		Task<OperationResult<Guid>> DeleteAsync(Guid id);
	}
}
