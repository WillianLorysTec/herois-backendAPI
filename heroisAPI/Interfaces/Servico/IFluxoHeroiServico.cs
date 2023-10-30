using FluentResults;
using Models.DTOs;
using Models.Entidades;
using Models.ViewModel;

namespace Interfaces.Servico
{
    public interface IFluxoHeroiServico
    {
        public Result CriarHeroi(HeroiDTO heroi);
        public List<Heroi> ListarHerois();
        public Result ExcluirHeroi(int idHeroi);
        public List<Superpoderes> ListarPoderes();
        HeroiViewModel ListarHeroiPorId(int idHeroi);
        Result AtualizarHeroi(HeroiDTO heroi);
    }
}
