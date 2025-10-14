using Application.Repositories;
using Core.Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.Sqlite
{
    public class AttemptRepository : IAttemptRepository
    {
        private readonly StoreDbContext _context;

        public AttemptRepository(StoreDbContext context)
        {
            _context = context;
        }

        public object Add(Attempt entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddAsync(Attempt attempt)
        {
            _context.Attempts.Add(attempt);
            await _context.SaveChangesAsync();
            return attempt.Id;
        }

        public long Count(Expression<Func<Attempt, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<long> CountAsync(Expression<Func<Attempt, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Attempt> FindAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<Attempt>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Attempt FindOne(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public Task<Attempt> FindOneAsync(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Attempt>> GetByGameIdAsync(int gameId)
        {
            return await _context.Attempts
                .Where(a => a.GameId == gameId)
                .OrderBy(a => a.AttemptedAt)
                .ToListAsync();
        }

        public void Remove(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public void Update(object id, Attempt entity)
        {
            throw new NotImplementedException();
        }

        Task<object> IRepository<Attempt>.AddAsync(Attempt entity)
        {
            throw new NotImplementedException();
        }
    }
}
