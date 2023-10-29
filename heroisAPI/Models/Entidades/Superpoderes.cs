using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entidades
{
    [Table("superpoderes")]
    public class Superpoderes
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Superpoder { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public virtual List<Heroi> Herois { get; set; } = new List<Heroi>();

    }
}
