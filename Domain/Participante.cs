using System.ComponentModel.DataAnnotations;

namespace DesafioTrica.Domain
{
    public class Participante
    {
        public Participante(int id, string nomeDoParticipante, decimal valorDaContribuicao, Churrasco churrasco)
        {
            Id = id;
            NomeDoParticipante = nomeDoParticipante;
            ValorDaContribuicao = valorDaContribuicao;
            Churrasco = churrasco;
        }

        public int Id { get; }

        [Required]
        public string NomeDoParticipante { get; set; }

        [Required]
        public decimal ValorDaContribuicao { get; set; }

        public Churrasco Churrasco { get; }

    }
}
