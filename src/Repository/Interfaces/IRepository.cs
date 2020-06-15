using System.Threading.Tasks;
using Domain.Models;

namespace Repository.Interfaces
{
    public interface IRepository<TEntity, TId>
        where TEntity: Entity
        where TId: struct 
    {
        Task<TEntity> GetByIdAsync(TId id);
        void Save(TEntity entity);
    }
}