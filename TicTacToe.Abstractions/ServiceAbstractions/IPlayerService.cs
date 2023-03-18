using System.Linq.Expressions;
using TicTacToe.Contracts.Commands;
using TicTacToe.Contracts.OperationResult;
using TicTacToe.Contracts.Responses;
using TicTacToe.Domain.Entities;

namespace TicTacToe.Abstractions.ServiceAbstractions
{
	public interface IPlayerService
	{
		Task<IEnumerable<PlayerResponse>> GetAllAsync(Expression<Func<Player, bool>>? filter = null);
		Task<IOperationResult<PlayerResponse>> GetAsync(Guid id);
		Task<IOperationResult<Guid>> CreateAsync(string name);
		Task<IOperationResult<PlayerResponse>> UpdateAsync(UpdatePlayerCommand command);
		Task<IOperationResult<Guid>> DeleteAsync(Guid id);
	}
}
