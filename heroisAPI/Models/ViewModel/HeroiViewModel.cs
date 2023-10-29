using Models.DTOs;

namespace Models.ViewModel
{
    public class HeroiViewModel
    {
        public int? Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string NomeHeroi { get; set; } = string.Empty;
        public float Peso { get; set; }
        public float Altura { get; set; }
        public DateTime DataNascimento { get; set; }
        public IEnumerable<SuperpoderViewModel> SuperPoderes { get; set; } = Enumerable.Empty<SuperpoderViewModel>();
    }
}
