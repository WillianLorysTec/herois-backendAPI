using Models.DTOs;

namespace Models.ViewModel
{
    public class HeroiViewModel
    {
        public int? Id { get; set; }
        public string NomeHeroi { get; set; } = string.Empty;
        public IEnumerable<SuperpoderViewModel> SuperPoderes { get; set; } = Enumerable.Empty<SuperpoderViewModel>();
    }
}
