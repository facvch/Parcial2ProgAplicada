using Domain.Entities;

public interface IGameApplicationService
{
    Task<int> RegisterPlayerAsync(string lastname, string firstname, int age); // retorna playerId
    Task<Game> StartGameAsync(int playerId);
    Task<Attempt> GuessNumberAsync(int gameId, string attemptedNumber);
}
