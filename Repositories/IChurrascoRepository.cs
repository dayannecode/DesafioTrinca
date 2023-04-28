using DesafioTrica.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioTrica.Repositories
{
    public interface IChurrascoRepository
    {
        Task<IEnumerable<Churrasco>> GetChurrascosAsync();
        Task<Churrasco> GetChurrascoAsync(int id);
        Task<int> AddChurrascoAsync(Churrasco churrasco);
        Task<bool> UpdateChurrascoAsync(Churrasco churrasco);
        Task<bool> DeleteChurrascoAsync(int id);
        Task<IEnumerable<Churrasco>> GetAllAsync();

    }
}
