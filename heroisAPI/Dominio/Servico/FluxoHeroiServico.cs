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
        public Heroi CriarHeroi(HeroiDTO heroi)
        {
            Heroi resultado = _repositorio.CriarHeroi(heroi);
            return resultado;
        }

        public bool ExcluirHeroi(int idHeroi)
        {
            return _repositorio.ExcluirHeroi(idHeroi);
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
