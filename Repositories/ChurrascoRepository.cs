using DesafioTrica.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioTrica.Repositories
{
    public class ChurrascoRepository : IChurrascoRepository
    {
        private readonly AppDbContext _dbContext;

        public ChurrascoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<Churrasco>> GetChurrascosAsync()
        {
            return await _dbContext.Churrascos.ToListAsync();
        }

        public async Task<Churrasco> GetChurrascoAsync(int id)
        {
            return await _dbContext.Churrascos.FindAsync(id);
        }

        public async Task<int> AddChurrascoAsync(Churrasco churrasco)
        {
            if (churrasco == null)
                throw new ArgumentNullException(nameof(churrasco));

            await _dbContext.Churrascos.AddAsync(churrasco);
            await _dbContext.SaveChangesAsync();
            return churrasco.Id;
        }

        public async Task<bool> UpdateChurrascoAsync(Churrasco churrasco)
        {
            if (churrasco == null)
                throw new ArgumentNullException(nameof(churrasco));

            var existingChurrasco = await _dbContext.Churrascos.FindAsync(churrasco.Id);
            if (existingChurrasco == null)
            {
                return false;
            }

            existingChurrasco.Descricao = churrasco.Descricao;
            existingChurrasco.Data = churrasco.Data;
            existingChurrasco.Observacoes = churrasco.Observacoes;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteChurrascoAsync(int id)
        {
            var existingChurrasco = await _dbContext.Churrascos.FindAsync(id);
            if (existingChurrasco == null)
            {
                return false;
            }

            _dbContext.Churrascos.Remove(existingChurrasco);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Churrasco>> GetAllAsync()
        {
            return await _dbContext.Churrascos.ToListAsync();
        }

        public async Task AdicionarParticipanteAsync(int churrascoId, Participante participante)
        {
            var churrasco = await GetChurrascoAsync(churrascoId);
            if (churrasco == null)
                throw new ArgumentException("Churrasco não encontrado.");

            if (participante == null)
                throw new ArgumentNullException(nameof(participante));

            churrasco.AdicionarParticipante(participante);

            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoverParticipanteAsync(int idChurrasco, int idParticipante)
        {
            var churrasco = await GetChurrascoAsync(idChurrasco);
            if (churrasco == null)
                throw new ArgumentException("Churrasco não encontrado.");

            var participante = churrasco.Participantes.FirstOrDefault(p => p.Id == idParticipante);
            if (participante == null)
                throw new ArgumentException("Participante não encontrado.");

            churrasco.RemoverParticipante(participante);

            await _dbContext.SaveChangesAsync();
        }

        public async Task AdicionarSugestaoContribuicaoAsync(int idChurrasco, decimal valorSugerido, bool bebidaInclusa)
        {
            var churrasco = await _dbContext.Churrascos.FindAsync(idChurrasco);

            if (churrasco == null)
                throw new ArgumentException("Churrasco não encontrado.");

            churrasco.ValorSugerido = valorSugerido;
            churrasco.BebidaInclusa = bebidaInclusa;

            await _dbContext.SaveChangesAsync();
        }

        public async Task AtualizarContribuicaoSugeridaAsync(int idChurrasco, decimal valorSugerido, bool bebidaInclusa)
        {
            var churrasco = await _dbContext.Churrascos.FindAsync(idChurrasco);

            if (churrasco == null)
                throw new ArgumentException("Churrasco não encontrado.");

            churrasco.ValorSugerido = valorSugerido;
            churrasco.BebidaInclusa = bebidaInclusa;

            await _dbContext.SaveChangesAsync();
        }

        public class ChurrascoDetalhes
        {
            public DateTime Data { get; set; }
            public string Descricao { get; set; }
            public string ObservacoesAdicionais { get; set; }
            public int NumeroDeParticipantes { get; set; }
            public decimal ValorArrecadado { get; set; }
        }

        public async Task<ChurrascoDetalhes> BuscarDetalhesChurrasco(int idChurrasco)
        {
            var churrasco = await _dbContext.Churrascos.Include(c => c.Participantes)
                            .FirstOrDefaultAsync(c => c.Id == idChurrasco);

            if (churrasco == null)
                throw new ArgumentException("Churrasco não encontrado.");

            var detalhes = new ChurrascoDetalhes
            {
                Data = churrasco.Data,
                Descricao = churrasco.Descricao,
                ObservacoesAdicionais = churrasco.Observacoes,
                NumeroDeParticipantes = churrasco.Participantes.Count,
                ValorArrecadado = churrasco.Participantes.Sum(p => p.ValorDaContribuicao)
            };

            return detalhes;
        }


    }
}
