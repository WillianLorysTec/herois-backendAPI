using FluentResults;
using Models.DTOs;
using Models.Entidades;
using Models.ViewModel;

namespace Interfaces.Repositorio
{
    public interface IFluxoHeroiRepositorio
    {
        public Result CriarHeroi(HeroiDTO heroi);
        public List<Heroi> ListarHerois();
        public HeroiViewModel ListarHeroiPorID(int idHeroi);
        public Result ExcluirHeroi(int idHeroi);
        List<Superpoderes> ListarPoderes();
        Result AtualizarHeroi(HeroiDTO heroi);
    }
}
