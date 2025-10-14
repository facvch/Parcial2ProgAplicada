using Application.Repositories;
using Core.Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.Sqlite
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly StoreDbContext _context;

        public PlayerRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Player player)
        {
            _context.Set<Player>().Add(player);
            await _context.SaveChangesAsync();
            return player.Id;
        }

        public async Task<IEnumerable<Player>> GetAllAsync()
        {
            return await _context.Set<Player>().ToListAsync();
        }

        public async Task<Player?> GetByNameAsync(string firstname, string lastname)
        {
            return await _context.Set<Player>()
                .FirstOrDefaultAsync(p => p.Firstname == firstname && p.Lastname == lastname);
        }

        public async Task<Player?> GetByIdAsync(int id)
        {
            return await _context.Set<Player>().FindAsync(id);
        }

        public object Add(Player entity)
        {
            throw new NotImplementedException();
        }

        Task<object> IRepository<Player>.AddAsync(Player entity)
        {
            throw new NotImplementedException();
        }

        public long Count(Expression<Func<Player, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<long> CountAsync(Expression<Func<Player, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Player> FindAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<Player>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Player FindOne(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public Task<Player> FindOneAsync(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public void Remove(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public void Update(object id, Player entity)
        {
            throw new NotImplementedException();
        }
    }
}
