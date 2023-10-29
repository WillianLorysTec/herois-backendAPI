using Models.DTOs;

namespace Models.ViewModel
{
    public class HeroiAtualizacaoViewModel
    {
        public int? Id { get; set; }
        public string NomeHeroi { get; set; } = string.Empty;
        public IEnumerable<SuperpoderAtualizacaoViewModel> SuperPoderes { get; set; } = Enumerable.Empty<SuperpoderAtualizacaoViewModel>();
    }
}
