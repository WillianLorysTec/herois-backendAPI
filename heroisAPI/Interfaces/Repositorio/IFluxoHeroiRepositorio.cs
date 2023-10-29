using Models.DTOs;
using Models.Entidades;
using Models.ViewModel;

namespace Interfaces.Repositorio
{
    public interface IFluxoHeroiRepositorio
    {
        public Heroi CriarHeroi(HeroiDTO heroi);
        public List<Heroi> ListarHerois();
        public HeroiViewModel ListarHeroiPorID(int idHeroi);
        public bool ExcluirHeroi(int idHeroi); // tratar retorno com fluentResult
        List<Superpoderes> ListarPoderes();
        void AtualizarHeroi(HeroiDTO heroi);
    }
}
