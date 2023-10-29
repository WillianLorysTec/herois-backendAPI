using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Models.Entidades
{
    [Table("herois")]
    public class Heroi
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public string NomeHeroi { get; set; } = string.Empty;

        [Required]
        public float Peso { get; set; }

        [Required]
        public float Altura { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }

        public virtual List<Superpoderes> SuperPoderes { get; set; } = new List<Superpoderes>();
    }
}
