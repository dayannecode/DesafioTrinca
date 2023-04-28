using System.Threading.Tasks;

namespace DesafioTrica.Interfaces.Services
{
    public interface IChurrascoService
    {
        Task<decimal> CalcularValorPorPessoaAsync(int idChurrasco);
    }
}
