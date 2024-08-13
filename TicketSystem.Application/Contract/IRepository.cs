using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.Application.Contract
{
    public interface IRepository<Tentity,Tid>
    {
        Task<Tentity> CreateAsync(Tentity entity);

        Task<Tentity> UpdateAsync(Tentity entity);

        Task<Tentity> DeleteAsync(Tentity entity);

        Task<Tentity> GetByIdAsync(Tid id);

        Task<IQueryable<Tentity>> GetAllAsync();

        Task<int> SaveChangesAsync();

    }
}
