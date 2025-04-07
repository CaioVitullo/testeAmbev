using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<SaleServiceDto> _sales;

        public SaleRepository(DbContext context)
        {
            _context = context;
            _sales = context.Set<SaleServiceDto>();
        }

        public async Task<SaleServiceDto> GetByIdAsync(int id)
        {
            return await _sales.FindAsync(id);
        }

        public async Task<IEnumerable<SaleServiceDto>> GetAllAsync()
        {
            return await _sales.ToListAsync();
        }

        public async Task AddAsync(SaleServiceDto entity)
        {
            await _sales.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SaleServiceDto entity)
        {
            _sales.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var sale = await GetByIdAsync(id);
            if (sale != null)
            {
                _sales.Remove(sale);
                await _context.SaveChangesAsync();
            }
        }

        public IQueryable<SaleServiceDto> Query()
        {
            return _sales.AsQueryable();
        }
    }
}
