using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IGuiaNumeroRepository
    {
         Task<int> GetLastGuiaIdAsync(string prestadorId);
    }
}