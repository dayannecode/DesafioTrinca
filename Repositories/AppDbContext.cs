using DesafioTrica.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioTrica.Repositories
{
    public class AppDbContext : DbContext, IChurrascoRepository
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Churrasco> Churrascos { get; set; }
        public DbSet<Participante> Participantes { get; set; }

        public async Task<IEnumerable<Churrasco>> GetChurrascosAsync()
        {
            return await Churrascos.ToListAsync();
        }

        public async Task<Churrasco> GetChurrascoAsync(int id)
        {
            return await Churrascos.FindAsync(id);
        }

        public async Task<int> AddChurrascoAsync(Churrasco churrasco)
        {
            await Churrascos.AddAsync(churrasco);
            await SaveChangesAsync();
            return churrasco.Id;
        }

        public async Task<bool> UpdateChurrascoAsync(Churrasco churrasco)
        {
            if (!await Churrascos.AnyAsync(e => e.Id == churrasco.Id))
            {
                return false;
            }

            Churrascos.Update(churrasco);
            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteChurrascoAsync(int id)
        {
            var churrasco = await Churrascos.FindAsync(id);
            if (churrasco == null)
            {
                return false;
            }

            Churrascos.Remove(churrasco);
            await SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Churrasco>> GetAllAsync()
        {
            return await Churrascos.ToListAsync();
        }


    }
}
