using Microsoft.AspNetCore.Mvc;
using TicTacToe.Abstractions.ServiceAbstractions;
using TicTacToe.Contracts.Commands;
using TicTacToe.Contracts.OperationResult;
using TicTacToe.Contracts.Responses;

namespace TicTacToe.API.Controllers
{
	public class GamesController : ApiControllerBase
	{
		private readonly IGameService _gameService;

		public GamesController(IGameService gameService)
		{
			_gameService = gameService;
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<GameResponse>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAllAsync()
		{
			return Ok(await _gameService.GetAllAsync());
		}

		[HttpGet("{id:guid}")]
		[ProducesResponseType(typeof(OperationResult<GameResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(OperationResult<GameResponse>), StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> GetAsync([FromRoute] Guid id)
		{
			var response = await _gameService.GetAsync(id);

			return response.Ok ? Ok(response) : BadRequest(response);
		}

		[HttpPost]
		[ProducesResponseType(typeof(OperationResult<Guid>), StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(OperationResult<Guid>), StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> CreateAsync([FromBody] CreateGameCommand command)
		{
			var response = await _gameService.CreateAsync(command);

			return response.Ok ? Created(HttpContext.Request.Path, response) : BadRequest(response);
		}

		[HttpPatch]
		[ProducesResponseType(typeof(OperationResult<GameResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(OperationResult<GameResponse>), StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> DoStepAsync([FromBody] DoStepCommand command)
		{
			var response = await _gameService.DoStepAsync(command);

			return response.Ok ? Ok(response) : BadRequest(response);
		}

		[HttpDelete("{id:guid}")]
		[ProducesResponseType(typeof(OperationResult<Guid>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(OperationResult<Guid>), StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
		{
			var response = await _gameService.DeleteAsync(id);

			return response.Ok ? Ok(response) : BadRequest(response);
		}
	}
}
