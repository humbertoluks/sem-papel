using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Repository.Interfaces
{
    public interface IStudentRepository: IRepository<Student, int>
    {
        void Delete(int id);
        Task<IEnumerable<Student>> All();
    }
}
