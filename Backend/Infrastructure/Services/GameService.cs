using Application.DataTransferObjects;
using Domain.DomainServices;
using Domain.Entities;
using ESCMB;
using Infrastructure.Repositories.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class GameService : IGameService
    {
        private readonly StoreDbContext _context;
        private readonly ILogger<GameService> _logger;
        private readonly Random _random;

        public GameService(StoreDbContext context, ILogger<GameService> logger)
        {
            _context = context;
            _logger = logger;
            _random = new Random();
        }

        public async Task<RegisterPlayerResponse> RegisterPlayerAsync(RegisterPlayerRequest request)
        {
            try
            {
                _logger.LogInformation("Intentando registrar jugador: {FirstName} {LastName}",
                    request.FirstName, request.LastName);

                // Verificar si el jugador ya existe
                var existingPlayer = await _context.Players
                    .FirstOrDefaultAsync(p => p.FirstName == request.FirstName &&
                                             p.LastName == request.LastName &&
                                             p.Age == request.Age);

                if (existingPlayer != null)
                {
                    _logger.LogWarning("Jugador ya registrado: {FirstName} {LastName}",
                        request.FirstName, request.LastName);
                    throw new InvalidOperationException("El jugador ya se encuentra registrado.");
                }

                var player = new Player
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Age = request.Age,
                    RegistrationDate = DateTime.UtcNow
                };

                _context.Players.Add(player);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Jugador registrado exitosamente con ID: {PlayerId}", player.PlayerId);

                return new RegisterPlayerResponse { PlayerId = player.PlayerId };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar jugador");
                throw;
            }
        }

        public async Task<StartGameResponse> StartGameAsync(StartGameRequest request)
        {
            try
            {
                _logger.LogInformation("Iniciando nuevo juego para jugador: {PlayerId}", request.PlayerId);

                // Validar que el jugador existe
                var player = await _context.Players.FindAsync(request.PlayerId);
                if (player == null)
                {
                    _logger.LogWarning("Jugador no encontrado: {PlayerId}", request.PlayerId);
                    throw new KeyNotFoundException("Jugador no encontrado.");
                }

                // Validar que no tenga juegos activos
                var activeGame = await _context.Games
                    .FirstOrDefaultAsync(g => g.PlayerId == request.PlayerId && !g.IsFinished);

                if (activeGame != null)
                {
                    _logger.LogWarning("Jugador {PlayerId} ya tiene un juego activo: {GameId}",
                        request.PlayerId, activeGame.GameId);
                    throw new InvalidOperationException("Ya tienes un juego activo. Debes terminarlo antes de empezar uno nuevo.");
                }

                var game = new Game
                {
                    PlayerId = request.PlayerId,
                    SecretNumber = GenerateSecretNumber(),
                    CreatedAt = DateTime.UtcNow,
                    IsFinished = false
                };

                _context.Games.Add(game);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Juego iniciado exitosamente: {GameId} para jugador: {PlayerId}",
                    game.GameId, request.PlayerId);

                return new StartGameResponse
                {
                    GameId = game.GameId,
                    PlayerId = game.PlayerId,
                    CreateAt = game.CreatedAt
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al iniciar juego para jugador: {PlayerId}", request.PlayerId);
                throw;
            }
        }

        public async Task<GuessNumberResponse> GuessNumberAsync(GuessNumberRequest request)
        {
            try
            {
                _logger.LogInformation("Procesando intento para juego: {GameId}, número: {AttemptedNumber}",
                    request.GameId, request.AttemptedNumber);

                var game = await _context.Games
                    .Include(g => g.Attempts)
                    .FirstOrDefaultAsync(g => g.GameId == request.GameId);

                if (game == null)
                {
                    _logger.LogWarning("Juego no encontrado: {GameId}", request.GameId);
                    throw new KeyNotFoundException("Juego no encontrado.");
                }

                if (game.IsFinished)
                {
                    _logger.LogWarning("Intento en juego ya finalizado: {GameId}", request.GameId);
                    throw new InvalidOperationException($"El juego {request.GameId} ya ha finalizado.");
                }

                // Validar formato del número
                if (!await IsValidNumberAsync(request.AttemptedNumber))
                {
                    _logger.LogWarning("Número inválido: {AttemptedNumber}", request.AttemptedNumber);
                    throw new ArgumentException("El número debe tener 4 dígitos y no pueden repetirse.");
                }

                // Usar ESCMB.GameCore para validar el intento
                var guessResult = ValidateGuess(game.SecretNumber, request.AttemptedNumber);

                var attempt = new Attempt
                {
                    GameId = request.GameId,
                    AttemptedNumber = request.AttemptedNumber,
                    AttemptDate = DateTime.UtcNow,
                    Result = guessResult // Usamos directamente el mensaje del resultado
                };

                _context.Attempts.Add(attempt);

                // Verificar si el jugador adivinó el número (4 famas)
                bool isCorrect = guessResult.Contains("¡Felicidades!") ||
                                guessResult.Contains("4 fama") ||
                                Evaluator.ValidateAttempt(game.SecretNumber, request.AttemptedNumber).Fama == 4;

                if (isCorrect)
                {
                    game.IsFinished = true;
                    _logger.LogInformation("Juego {GameId} finalizado correctamente", request.GameId);
                }

                await _context.SaveChangesAsync();

                _logger.LogInformation("Intento procesado: {Message}", guessResult);

                return new GuessNumberResponse
                {
                    GameId = request.GameId,
                    AttemptedNumber = request.AttemptedNumber,
                    Message = guessResult
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar intento para juego: {GameId}", request.GameId);
                throw;
            }
        }

        public async Task<bool> PlayerExistsAsync(int playerId)
        {
            return await _context.Players.AnyAsync(p => p.PlayerId == playerId);
        }

        public async Task<bool> HasActiveGameAsync(int playerId)
        {
            return await _context.Games.AnyAsync(g => g.PlayerId == playerId && !g.IsFinished);
        }

        public async Task<bool> IsValidNumberAsync(string number)
        {
            if (string.IsNullOrEmpty(number) || number.Length != 4 || !number.All(char.IsDigit))
                return false;

            return number.Distinct().Count() == 4;
        }

        private string GenerateSecretNumber()
        {
            var digits = Enumerable.Range(0, 10).OrderBy(x => _random.Next()).Take(4).ToArray();
            return string.Join("", digits);
        }

        // Método que valida el numero ingresado con ESCMB.GameCore
        private string ValidateGuess(string secret, string attempt)
        {
            try
            {
                var resultado = Evaluator.ValidateAttempt(secret, attempt);
                return resultado.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar intento con GameCore");
                return "Error al procesar el intento.";
            }
        }

        // Método alternativo si necesitas acceso a Famas y Picas por separado
        private (string Message, bool IsCorrect) ValidateGuessWithDetails(string secret, string attempt)
        {
            try
            {
                var resultado = Evaluator.ValidateAttempt(secret, attempt);
                bool isCorrect = resultado.Fama == 4;
                return (resultado.Message, isCorrect);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar intento con GameCore");
                return ("Error al procesar el intento.", false);
            }
        }
    }
}
