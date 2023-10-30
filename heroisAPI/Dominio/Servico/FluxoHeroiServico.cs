using FluentResults;
using Interfaces.Servico;
using Models.DTOs;
using Models.Entidades;
using Models.ViewModel;

namespace Servico
{
    public class FluxoHeroiServico : IFluxoHeroiServico
    {
        private readonly Interfaces.Repositorio.IFluxoHeroiRepositorio _repositorio;

        public FluxoHeroiServico(Interfaces.Repositorio.IFluxoHeroiRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public Result AtualizarHeroi(HeroiDTO heroi)
        {
            Result resultado = _repositorio.AtualizarHeroi(heroi);
            return resultado;
        }

        public Result CriarHeroi(HeroiDTO heroi)
        {
            Result resultado = _repositorio.CriarHeroi(heroi);
            return resultado;
        }

        public Result ExcluirHeroi(int idHeroi)
        {
            Result resultado = _repositorio.ExcluirHeroi(idHeroi);
            return resultado;
        }

        public HeroiViewModel ListarHeroiPorId(int idHeroi)
        {
            return _repositorio.ListarHeroiPorID(idHeroi);
        }

        public List<Heroi> ListarHerois()
        {
            return _repositorio.ListarHerois();
        }

        public List<Superpoderes> ListarPoderes()
        {
            return _repositorio.ListarPoderes();
        }
    }
}
