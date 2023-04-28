using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DesafioTrica.Domain
{
    public class Churrasco
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A data é obrigatória.")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Descricao { get; set; }

        public string Observacoes { get; set; }

        [Display(Name = "Contribuição sugerida")]
        public decimal ContribuicaoSugerida { get; set; }

        [Display(Name = "Contribuição sugerida com bebida")]
        public decimal ContribuicaoSugeridaComBebida { get; set; }

        public decimal ValorSugerido { get; set; }
        public bool BebidaInclusa { get; set; }

        public decimal ValorTotal
        {
            get
            {
                decimal total = 0;
                foreach (var participante in Participantes)
                {
                    total += participante.ValorDaContribuicao;
                }
                return total;
            }
        }

        public virtual ICollection<Participante> Participantes { get; set; } = new List<Participante>();
        public DateTime DataDoChurrasco { get; internal set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Churrasco other))
                return false;

            return Id == other.Id
                && Descricao == other.Descricao
                && Data == other.Data
                && Observacoes == other.Observacoes
                && ValorTotal == other.ValorTotal
                && Participantes.SequenceEqual(other.Participantes);
        }

        public void CalcularContribuicao()
        {
            var totalContribuicoes = Participantes.Sum(p => p.ValorDaContribuicao);

            if (BebidaInclusa)
                ValorSugerido = ContribuicaoSugeridaComBebida;
            else
                ValorSugerido = ContribuicaoSugerida;

            var diferenca = ValorSugerido - totalContribuicoes;
            var numeroParticipantes = Participantes.Count();

            if (diferenca > 0 && numeroParticipantes > 0)
            {
                var contribuicaoAdicional = diferenca / numeroParticipantes;
                Participantes.ToList().ForEach(p => p.ValorDaContribuicao += contribuicaoAdicional);
            }
        }

        public void AdicionarParticipante(Participante participante)
        {
            Participantes.Add(participante);
        }

        public void RemoverParticipante(Participante participante)
        {
            Participantes.Remove(participante);
        }

    }
}
