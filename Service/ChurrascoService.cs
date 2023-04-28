using DesafioTrica.Interfaces.Services;
using DesafioTrica.Repositories;

namespace DesafioTrica.Service
{
    public class ChurrascoService : IChurrascoService
    {
        private readonly IChurrascoRepository _churrascoRepository;

        public ChurrascoService(IChurrascoRepository churrascoRepository)
        {
            _churrascoRepository = churrascoRepository;
        }

        public async Task<decimal> CalcularValorPorPessoaAsync(int idChurrasco)
        {
            var churrasco = await _churrascoRepository.GetChurrascoAsync(idChurrasco);

            if (churrasco == null)
                throw new ArgumentException("Churrasco não encontrado.");

            var valorTotal = 0m; 

            foreach (var participante in churrasco.Participantes)
            {
                valorTotal += participante.ValorDaContribuicao; 
            }

            var qtdPessoas = churrasco.Participantes.Count();
            if (qtdPessoas == 0)
                throw new ArgumentException("O churrasco não tem pessoas.");

            var valorPorPessoa = valorTotal / qtdPessoas;

            return valorPorPessoa;
        }


    }

}
