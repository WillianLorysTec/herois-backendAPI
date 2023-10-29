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
        public bool ExcluirHeroi(int idHeroi); // tratar retorno com fluentResult
        public List<Superpoderes> ListarPoderes();
        HeroiViewModel ListarHeroiPorId(int idHeroi);
        void AtualizarHeroi(HeroiDTO heroi);
    }
}
