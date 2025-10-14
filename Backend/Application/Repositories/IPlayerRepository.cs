using Core.Application.Repositories;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Task<int> AddAsync(Player player);
        Task<IEnumerable<Player>> GetAllAsync();
        Task<Player?> GetByIdAsync(int id);

        // NUEVOS:
        Task<Player?> GetByNameAsync(string firstname, string lastname);
    }
}
