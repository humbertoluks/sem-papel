using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Repository.Interfaces;

namespace Repository.Interfaces
{
    public interface IGuiaRepository:IRepository<Guia, int>
    {
        Task<IEnumerable<Guia>> All();

        void Delete(int id);
    }
}