using Application.Repositories;
using Core.Infraestructure.Repositories.Sql;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories.Sql
{
    public class AutomovilRepository : IAutomovilRepository
    {
        private readonly StoreDbContext _context;
        public AutomovilRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Automovil automovil)
        {
            await _context.Automoviles.AddAsync(automovil);
            await _context.SaveChangesAsync();
        }

        public async Task<Automovil> GetByIdAsync(int id)
        {
            return await _context.Automoviles.FindAsync(id);
        }

        public async Task<IEnumerable<Automovil>> GetAllAsync()
        {
            return await _context.Automoviles.ToListAsync();
        }

        public async Task UpdateAsync(Automovil automovil)
        {
            _context.Automoviles.Update(automovil);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Automoviles.FindAsync(id);
            if (entity != null)
            {
                _context.Automoviles.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Automovil> GetByChasisAsync(string numeroChasis)
        {
            return await _context.Automoviles.FirstOrDefaultAsync(a => a.NumeroChasis == numeroChasis);
        }

        public async Task<Automovil> GetByMotorAsync(string numeroMotor)
        {
            return await _context.Automoviles
                .FirstOrDefaultAsync(a => a.NumeroMotor == numeroMotor);
        }
    }
}
