using Microsoft.AspNetCore.Mvc;
using TicTacToe.Abstractions.ServiceAbstractions;
using TicTacToe.Contracts.Commands;
using TicTacToe.Contracts.OperationResult;
using TicTacToe.Contracts.Responses;

namespace TicTacToe.API.Controllers
{
	public class PlayersController : ApiControllerBase
	{
		private readonly IPlayerService _playerService;

		public PlayersController(IPlayerService playerService)
		{
			_playerService = playerService;
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<GameResponse>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAllAsync()
		{
			return Ok(await _playerService.GetAllAsync());
		}

		[HttpGet("{id:guid}")]
		[ProducesResponseType(typeof(IOperationResult<PlayerResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(IOperationResult<PlayerResponse>), StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> GetAsync([FromRoute] Guid id)
		{
			var response = await _playerService.GetAsync(id);

			return response.Ok ? Ok(response) : BadRequest(response);
		}

		[HttpPost]
		[ProducesResponseType(typeof(IOperationResult<Guid>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(IOperationResult<Guid>), StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> CreateAsync([FromBody] string name)
		{
			var response = await _playerService.CreateAsync(name);

			return response.Ok ? Created(HttpContext.Request.Path, response) : BadRequest(response);
		}

		[HttpPatch]
		[ProducesResponseType(typeof(IOperationResult<PlayerResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(IOperationResult<PlayerResponse>), StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> UpdateAsync([FromBody] UpdatePlayerCommand command)
		{
			var response = await _playerService.UpdateAsync(command);

			return response.Ok ? Ok(response) : BadRequest(response);
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
		{
			var response = await _playerService.DeleteAsync(id);

			return response.Ok ? Ok(response) : BadRequest(response);
		}
	}
}
