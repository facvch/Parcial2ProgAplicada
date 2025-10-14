using Application.Repositories;
using Core.Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.Sqlite
{
    public class GameRepository : IGameRepository
    {
        private readonly StoreDbContext _context;

        public GameRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Game game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return game.Id;
        }

        public async Task<Game?> GetByIdAsync(int id)
        {
            return await _context.Games
                .Include(g => g.Player)
                .Include(g => g.Attempts)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Game?> GetActiveGameByPlayerAsync(int playerId)
        {
            return await _context.Games
                .Include(g => g.Attempts)
                .FirstOrDefaultAsync(g => g.PlayerId == playerId && !g.IsFinished);
        }

        public async Task UpdateAsync(Game game)
        {
            _context.Games.Update(game);
            await _context.SaveChangesAsync();
        }

        public object Add(Game entity)
        {
            throw new NotImplementedException();
        }

        Task<object> IRepository<Game>.AddAsync(Game entity)
        {
            throw new NotImplementedException();
        }

        public long Count(Expression<Func<Game, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<long> CountAsync(Expression<Func<Game, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Game> FindAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<Game>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Game FindOne(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public Task<Game> FindOneAsync(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public void Remove(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public void Update(object id, Game entity)
        {
            throw new NotImplementedException();
        }
    }
}
