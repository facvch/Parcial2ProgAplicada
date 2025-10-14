using Application.Repositories;
using Domain.Entities;
using ESCMB;
using Microsoft.Extensions.Logging;

public class GameApplicationService : IGameApplicationService
{
    private readonly IPlayerRepository _playerRepo;
    private readonly IGameRepository _gameRepo;
    private readonly IAttemptRepository _attemptRepo;
    private readonly ILogger<GameApplicationService> _logger;

    public GameApplicationService(IPlayerRepository playerRepo, IGameRepository gameRepo, IAttemptRepository attemptRepo, ILogger<GameApplicationService> logger)
    {
        _playerRepo = playerRepo;
        _gameRepo = gameRepo;
        _attemptRepo = attemptRepo;
        _logger = logger;
    }

    public Task<Attempt> GuessNumberAsync(int gameId, string attemptedNumber)
    {
        throw new NotImplementedException();
    }

    public async Task<int> RegisterPlayerAsync(string lastname, string firstname, int age)
    {
        if (string.IsNullOrWhiteSpace(lastname) || string.IsNullOrWhiteSpace(firstname) || age <= 0)
            throw new ArgumentException("Todos los campos son requeridos");

        // Verificar existencia (ej: por nombre+apellido)
        var exists = await _playerRepo.GetByNameAsync(firstname, lastname);
        if (exists != null)
        {
            _logger.LogInformation("Registro ignorado: jugador ya existe {firstname} {lastname}", firstname, lastname);
            return exists.Id;
        }

        var player = new Player { Firstname = firstname, Lastname = lastname, Age = age, RegisteredAt = DateTime.UtcNow };
        await _playerRepo.AddAsync(player);
        _logger.LogInformation("Jugador registrado Id={id}", player.Id);
        return player.Id;
    }

    public async Task<Game> StartGameAsync(int playerId)
    {
        // validar playerId
        var player = await _playerRepo.GetByIdAsync(playerId) ?? throw new KeyNotFoundException("Player not found");
        var active = await _gameRepo.GetActiveGameByPlayerAsync(playerId);
        if (active != null) throw new InvalidOperationException("El jugador ya tiene un juego activo.");

        // generar secreto de 4 dígitos sin repetidos
        var secret = GenerateSecretNumber();

        var game = new Game { PlayerId = playerId, SecretNumber = secret, CreatedAt = DateTime.UtcNow };
        await _gameRepo.AddAsync(game);
        _logger.LogInformation("Juego creado Id={gameId} Player={playerId}", game.Id, playerId);
        return game;
    }
/*
    public async Task<Attempt> GuessNumberAsync(int gameId, string attemptedNumber)
    {
        var game = await _gameRepo.GetByIdAsync(gameId) ?? throw new KeyNotFoundException("Game not found");
        if (game.IsFinished) throw new InvalidOperationException($"El juego {gameId} ya ha finalizado.");

        if (!IsValidAttempt(attemptedNumber)) throw new ArgumentException("El número debe tener 4 dígitos únicos.");

        // Usando ESCMB.GuessCore
        
        var engine = new GuessEngine(); // según biblioteca
        var result = engine.Evaluate(game.SecretNumber, attemptedNumber); // asumimos Evaluate devuelve Famas,Picas,Message
        var message = result.Message;
        

        var attempt = new Attempt { GameId = gameId, AttemptedNumber = attemptedNumber, Message = message, AttemptedAt = DateTime.UtcNow };
        await _attemptRepo.AddAsync(attempt);

        _logger.LogInformation("Game {gameId} attempt {num} => {msg}", gameId, attemptedNumber, message);

        if (result.Famas == 4)
        {
            game.IsFinished = true;
            await _gameRepo.UpdateAsync(game);
            _logger.LogInformation("Game {gameId} finished by guess", gameId);
        }

        return attempt;
    }
*/
    // helpers
    private string GenerateSecretNumber()
    {
        var rnd = new Random();
        var digits = new List<int>();
        while (digits.Count < 4) { var d = rnd.Next(0, 10); if (!digits.Contains(d)) digits.Add(d); }
        return string.Concat(digits);
    }

    private bool IsValidAttempt(string num) => num != null && num.Length == 4 && num.All(char.IsDigit) && num.Distinct().Count() == 4;
}
