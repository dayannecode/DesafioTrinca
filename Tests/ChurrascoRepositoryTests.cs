using DesafioTrica.Domain;
using DesafioTrica.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace ChurrascoManager.Tests
{
    public class ChurrascoRepositoryTests
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturnAllChurrascos()
        {
            var churrascos = new List<Churrasco>
            {
                new Churrasco { Id = 1, Descricao = "Churrasco 1" },
                new Churrasco { Id = 2, Descricao = "Churrasco 2" },
                new Churrasco { Id = 3, Descricao = "Churrasco 3" }
            };
            var mockDbSet = new Mock<DbSet<Churrasco>>();
            mockDbSet.As<IQueryable<Churrasco>>().Setup(m => m.Provider).Returns(churrascos.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Churrasco>>().Setup(m => m.Expression).Returns(churrascos.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Churrasco>>().Setup(m => m.ElementType).Returns(churrascos.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Churrasco>>().Setup(m => m.GetEnumerator()).Returns(() => churrascos.AsQueryable().GetEnumerator());

            var mockDbContext = new Mock<AppDbContext>();
            mockDbContext.Setup(m => m.Churrascos).Returns(mockDbSet.Object);

            var repository = new ChurrascoRepository(mockDbContext.Object);

            var result = await repository.GetAllAsync();

            Assert.True(churrascos.SequenceEqual(result));

        }

        [Fact]
        public async Task AdicionarChurrascoAsync_DeveAdicionarNovoChurrasco()
        {
            var churrasco = new Churrasco { Data = DateTime.Now, Descricao = "Churrasco Teste", Observacoes = "Churrasco de teste" };
            var mockDbSet = new Mock<DbSet<Churrasco>>();
            var mockDbContext = new Mock<AppDbContext>();
            mockDbContext.Setup(m => m.Churrascos).Returns(mockDbSet.Object);

            var repository = new ChurrascoRepository(mockDbContext.Object);

            await repository.AddChurrascoAsync(churrasco);

            mockDbSet.Verify(m => m.AddAsync(churrasco, default), Times.Once);
            mockDbContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }


    }

}


