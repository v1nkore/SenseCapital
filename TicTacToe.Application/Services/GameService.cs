using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicTacToe.Abstractions.ServiceAbstractions;
using TicTacToe.Application.Constants;
using TicTacToe.Contracts.Commands;
using TicTacToe.Contracts.OperationResult;
using TicTacToe.Contracts.Responses;
using TicTacToe.Domain.Entities;
using TicTacToe.Domain.Enums;
using TicTacToe.Domain.Extensions;
using TicTacToe.Infrastructure.Persistence;

namespace TicTacToe.Application.Services
{
	public class GameService : IGameService
	{
		private readonly TicTacToeDbContext _dbContext;
		private readonly IMapper _mapper;

		public GameService(TicTacToeDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<IEnumerable<GameResponse>> GetAllAsync(Expression<Func<Game, bool>>? filter = null)
		{
			return filter is null
				? _mapper.Map<IEnumerable<GameResponse>>(
					await _dbContext.Games
						.Include(x => x.BluePlayer)
						.Include(x => x.RedPlayer)
						.ToArrayAsync())
				: _mapper.Map<IEnumerable<GameResponse>>(
					await _dbContext.Games
						.Where(filter)
						.Include(x => x.BluePlayer)
						.Include(x => x.RedPlayer)
						.ToArrayAsync());
		}

		public async Task<OperationResult<GameResponse>> GetAsync(Guid id)
		{
			var game = await _dbContext.Games
				.Include(x => x.BluePlayer)
				.Include(x => x.RedPlayer)
				.FirstOrDefaultAsync(x => x.Id == id);

			if (game is null)
			{
				return new OperationResult<GameResponse>(null!, false).AddMetadata(GameServiceErrors.GameNotFound);
			}

			return new OperationResult<GameResponse>(_mapper.Map<GameResponse>(game), true);
		}

		public async Task<OperationResult<Guid>> CreateAsync(CreateGameCommand command)
		{
			if (command.BluePlayerId == command.RedPlayerId)
			{
				return new OperationResult<Guid>(Guid.Empty, false).AddMetadata(GameServiceErrors.SamePlayers);
			}
			var bluePlayer = await _dbContext.Players.FirstOrDefaultAsync(x => x.Id == command.BluePlayerId);
			var redPlayer = await _dbContext.Players.FirstOrDefaultAsync(x => x.Id == command.RedPlayerId);
			if (bluePlayer is null || redPlayer is null)
			{
				return new OperationResult<Guid>(Guid.Empty, false).AddMetadata(PlayerServiceErrors.PlayerNotFound);
			}

			var game = _mapper.Map<Game>(command);
			bluePlayer.GamesLikeBluePlayer.InitIfNullAndAdd(game);
			redPlayer.GamesLikeRedPlayer.InitIfNullAndAdd(game);
			game.PlayerTurn = new Random().Next(1, 2) % 2 == 0 ? bluePlayer.Id : redPlayer.Id;

			await _dbContext.Games.AddAsync(game);
			await _dbContext.SaveChangesAsync();

			return new OperationResult<Guid>(game.Id, true);
		}

		public async Task<OperationResult<GameResponse>> DoStepAsync(DoStepCommand command)
		{
			var game = await _dbContext.Games.FirstOrDefaultAsync(x => x.Id == command.GameId);
			if (game is null)
			{
				return new OperationResult<GameResponse>(null!, false).AddMetadata(GameServiceErrors.GameNotFound);
			}

			if (game.Status == GameStatus.Ended)
			{
				return new OperationResult<GameResponse>(_mapper.Map<GameResponse>(game), false).AddMetadata(GameServiceErrors.GameEnded);
			}

			var player = await _dbContext.Players.FirstOrDefaultAsync(x => x.Id == command.PlayerId);
			if (player is null)
			{
				return new OperationResult<GameResponse>(null!, false).AddMetadata(PlayerServiceErrors.PlayerNotFound);
			}
			if (game.PlayerTurn != player.Id)
			{
				return new OperationResult<GameResponse>(
						_mapper.Map<GameResponse>(game), false)
					.AddMetadata(GameServiceErrors.TurnError);
			}

			var alreadyInGame = await _dbContext.Games
				.CountAsync(x => (x.BluePlayerId == player.Id || x.RedPlayerId == player.Id)
								 && x.Id != game.Id && x.Status == GameStatus.Started) > 0;
			if (alreadyInGame)
			{
				return new OperationResult<GameResponse>(null!, false).AddMetadata(GameServiceErrors.PlayerInGame);
			}

			if (game.CurrentState[(command.Cell - 1) / 3][(command.Cell - 1) % 3] is '0' or 'X')
			{
				return new OperationResult<GameResponse>(_mapper.Map<GameResponse>(game), false).AddMetadata(GameServiceErrors.CellMarked);
			}

			if (game.BluePlayerId == player.Id)
			{
				var newRow = game.CurrentState[(command.Cell - 1) / 3].ToCharArray();
				newRow[(command.Cell - 1) % 3] = 'X';
				game.CurrentState[(command.Cell - 1) / 3] = new string(newRow);
				game.PlayerTurn = game.RedPlayerId;
			}
			else
			{
				var newRow = game.CurrentState[(command.Cell - 1) / 3].ToCharArray();
				newRow[(command.Cell - 1) % 3] = '0';
				game.CurrentState[(command.Cell - 1) / 3] = new string(newRow);
				game.PlayerTurn = game.BluePlayerId;
			}

			game.StepCount++;
			if (game.Status == GameStatus.Planned)
			{
				game.Status = GameStatus.Started;
			}
			else if (game.StepCount >= 5 && game.CheckWin())
			{
				game.Status = GameStatus.Ended;
			}

			_dbContext.Entry(game).State = EntityState.Modified;
			await _dbContext.SaveChangesAsync();

			return new OperationResult<GameResponse>(_mapper.Map<GameResponse>(game), true);
		}

		public async Task<OperationResult<Guid>> DeleteAsync(Guid id)
		{
			var game = await _dbContext.Games.FirstOrDefaultAsync(x => x.Id == id);
			if (game is null)
			{
				return new OperationResult<Guid>(id, false);
			}

			_dbContext.Games.Remove(game);
			await _dbContext.SaveChangesAsync();

			return new OperationResult<Guid>(id, true);
		}
	}
}
