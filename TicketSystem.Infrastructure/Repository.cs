using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Application.Contract;
using TicketSystem.Context;

namespace TicketSystem.Infrastructure
{
    public class Repository<Tentity, Tid> : IRepository<Tentity, Tid> where Tentity : class
    {
         protected readonly ApplicationContext _applicationContext;
         protected readonly DbSet<Tentity> _dbSet;

        public Repository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
            _dbSet = _applicationContext.Set<Tentity>();
        }
        public async Task<Tentity> CreateAsync(Tentity entity)
        {
            return (await _dbSet.AddAsync(entity)).Entity;
        }

        public Task<Tentity> UpdateAsync(Tentity entity)
        {
            return Task.FromResult(_dbSet.Update(entity).Entity);
        }

        public Task<Tentity> DeleteAsync(Tentity entity)
        {
            return Task.FromResult(_dbSet.Remove(entity).Entity);
        }

        public Task<IQueryable<Tentity>> GetAllAsync()
        {
            return Task.FromResult(_dbSet.Select(d => d));
        }

        public async Task<Tentity> GetByIdAsync(Tid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _applicationContext.SaveChangesAsync();
        }

       
    }
}
