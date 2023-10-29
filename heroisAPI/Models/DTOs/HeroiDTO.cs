using Models.Entidades;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Models.DTOs
{
    public class HeroiDTO
    {
        [Key]
        public int Id { get; set; }

        [Required, NotNull]
        public string Nome { get; set; } = string.Empty;

        [Required, NotNull]
        public string NomeHeroi { get; set; } = string.Empty;

        [Required, NotNull]
        public float Peso { get; set; }

        [Required, NotNull]
        public float Altura { get; set; }

        [Required, NotNull]
        public DateTime DataNascimento { get; set; }

        [Required, NotNull]
        public virtual List<int> PoderesAdicionados { get; set; } = new();
    }

}
