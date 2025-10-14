using Core.Application.Repositories;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IAttemptRepository : IRepository<Attempt>
    {
        Task<int> AddAsync(Attempt attempt);
        Task<IEnumerable<Attempt>> GetByGameIdAsync(int gameId);
    }
}
