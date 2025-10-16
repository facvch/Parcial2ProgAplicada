using Application.DataTransferObjects;
using Application.ApplicationServices;
using Filters;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("api/game/v1")]
    [ServiceFilter(typeof(BaseExceptionFilter))]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly ILogger<GameController> _logger;

        public GameController(IGameService gameService, ILogger<GameController> logger)
        {
            _gameService = gameService;
            _logger = logger;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(RegisterPlayerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterPlayer([FromBody] RegisterPlayerRequest request)
        {
            try
            {
                _logger.LogInformation("Solicitud de registro recibida");
                var result = await _gameService.RegisterPlayerAsync(request);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Error de operación en registro");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno en registro");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpPost("start")]
        [ProducesResponseType(typeof(StartGameResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> StartGame([FromBody] StartGameRequest request)
        {
            try
            {
                _logger.LogInformation("Solicitud de inicio de juego recibida para jugador: {PlayerId}", request.PlayerId);
                var result = await _gameService.StartGameAsync(request);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Jugador no encontrado");
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Error de operación en inicio de juego");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno en inicio de juego");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpPost("guess")]
        [ProducesResponseType(typeof(GuessNumberResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GuessNumber([FromBody] GuessNumberRequest request)
        {
            try
            {
                _logger.LogInformation("Solicitud de adivinanza recibida para juego: {GameId}", request.GameId);
                var result = await _gameService.GuessNumberAsync(request);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Juego no encontrado");
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Argumento inválido en adivinanza");
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Error de operación en adivinanza");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno en adivinanza");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }
    }
}
