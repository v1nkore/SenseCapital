using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicTacToe.Abstractions.ServiceAbstractions;
using TicTacToe.Application.Constants;
using TicTacToe.Contracts.Commands;
using TicTacToe.Contracts.OperationResult;
using TicTacToe.Contracts.Responses;
using TicTacToe.Domain.Entities;
using TicTacToe.Infrastructure.Persistence;

namespace TicTacToe.Application.Services
{
	public class PlayerService : IPlayerService
	{
		private readonly TicTacToeDbContext _dbContext;
		private readonly IMapper _mapper;

		public PlayerService(TicTacToeDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<IEnumerable<PlayerResponse>> GetAllAsync(Expression<Func<Player, bool>>? filter = null)
		{
			return filter is null
				? _mapper.Map<IEnumerable<PlayerResponse>>(
					await _dbContext.Players
						.Include(x => x.GamesLikeBluePlayer!)
							.ThenInclude(x => x.RedPlayer)
						.Include(x => x.GamesLikeRedPlayer!)
							.ThenInclude(x => x.BluePlayer)
						.ToArrayAsync())
				: _mapper.Map<IEnumerable<PlayerResponse>>(
					await _dbContext.Players
						.Where(filter)
						.Include(x => x.GamesLikeBluePlayer!)
							.ThenInclude(x => x.RedPlayer)
						.Include(x => x.GamesLikeRedPlayer!)
							.ThenInclude(x => x.BluePlayer)
						.ToArrayAsync());
		}

		public async Task<IOperationResult<PlayerResponse>> GetAsync(Guid id)
		{
			var player = await _dbContext.Players
				.Include(x => x.GamesLikeBluePlayer!)
					.ThenInclude(x => x.RedPlayer)
				.Include(x => x.GamesLikeRedPlayer!)
					.ThenInclude(x => x.BluePlayer)
				.FirstOrDefaultAsync(x => x.Id == id);

			if (player is null)
			{
				return new OperationResult<PlayerResponse>(null!, false).AddMetadata(PlayerServiceErrors.PlayerNotFound);
			}

			return new OperationResult<PlayerResponse>(_mapper.Map<PlayerResponse>(player), true);
		}

		public async Task<IOperationResult<Guid>> CreateAsync(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return await Task.FromResult(new OperationResult<Guid>(Guid.Empty, false).AddMetadata(PlayerServiceErrors.InvalidPlayerName));
			}

			var newPlayer = new Player() { Name = name };
			await _dbContext.Players.AddAsync(newPlayer);
			await _dbContext.SaveChangesAsync();

			return new OperationResult<Guid>(newPlayer.Id, true);
		}

		public async Task<IOperationResult<PlayerResponse>> UpdateAsync(UpdatePlayerCommand command)
		{
			if (string.IsNullOrEmpty(command.Name))
			{
				return await Task.FromResult(new OperationResult<PlayerResponse>(null!, false).AddMetadata(PlayerServiceErrors.InvalidPlayerName));
			}

			var player = await _dbContext.Players.FirstOrDefaultAsync(x => x.Id == command.Id);
			if (player is null)
			{
				return new OperationResult<PlayerResponse>(null!, false).AddMetadata(PlayerServiceErrors.PlayerNotFound);
			}

			player.Name = command.Name;
			_dbContext.Entry(player).State = EntityState.Modified;
			await _dbContext.SaveChangesAsync();

			return new OperationResult<PlayerResponse>(new PlayerResponse() { Id = player.Id, Name = player.Name }, true);
		}

		public async Task<IOperationResult<Guid>> DeleteAsync(Guid id)
		{
			var player = await _dbContext.Players.FirstOrDefaultAsync(x => x.Id == id);
			if (player is null)
			{
				return new OperationResult<Guid>(id, false).AddMetadata(PlayerServiceErrors.PlayerNotFound);
			}

			_dbContext.Players.Remove(player);
			await _dbContext.SaveChangesAsync();

			return new OperationResult<Guid>(id, true);
		}
	}
}
