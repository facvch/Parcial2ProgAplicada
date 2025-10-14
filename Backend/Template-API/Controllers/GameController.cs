using Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/game/v1")]
public class GameController : BaseController
{
    private readonly IGameApplicationService _gameService;

    public GameController(IGameApplicationService gameService)
    {
        _gameService = gameService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterPlayerRequest req)
    {
        if (req == null || string.IsNullOrWhiteSpace(req.Firstname) || string.IsNullOrWhiteSpace(req.Lastname) || req.Age <= 0)
            return BadRequest();

        try
        {
            var playerId = await _gameService.RegisterPlayerAsync(req.Lastname, req.Firstname, req.Age);
            return Ok(new { playerid = playerId });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPost("start")]
    public async Task<IActionResult> Start([FromBody] StartGameRequest req)
    {
        if (req == null || req.PlayerId <= 0) return BadRequest();

        try
        {
            var game = await _gameService.StartGameAsync(req.PlayerId);
            return Ok(new { gameid = game.Id, playerid = game.PlayerId, createdat = game.CreatedAt });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = "Player not found" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPost("guess")]
    public async Task<IActionResult> Guess([FromBody] GuessNumberRequest req)
    {
        if (req == null || req.GameId <= 0) return BadRequest();
        var attemptedString = req.AttemptedNumber.ToString().PadLeft(4, '0'); // ensure 4 digits

        try
        {
            var attempt = await _gameService.GuessNumberAsync(req.GameId, attemptedString);
            return Ok(new { gameid = attempt.GameId, attemptedNumber = attempt.AttemptedNumber, message = attempt.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = "Game not found" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}
