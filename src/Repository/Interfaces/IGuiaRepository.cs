using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Repository.Interfaces
{
    public interface IGuiaRepository
    {
        Task<IEnumerable<Guia>> All();

        void Delete(decimal id);
        Task<Guia> GetByIdAsync(decimal id);
        void Save(Guia entity);
    }
}