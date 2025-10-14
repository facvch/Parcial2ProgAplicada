using Core.Application.Repositories;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IGameRepository : IRepository<Game>
    {
        Task<Game?> GetByIdAsync(int id);
        Task<Game?> GetActiveGameByPlayerAsync(int playerId);
        Task<int> AddAsync(Game game);
        Task UpdateAsync(Game game);
    }
}
