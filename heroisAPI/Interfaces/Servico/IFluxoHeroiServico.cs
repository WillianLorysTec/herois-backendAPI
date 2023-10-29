using Models.DTOs;
using Models.Entidades;

namespace Interfaces.Servico
{
    public interface IFluxoHeroiServico
    {
        public Heroi CriarHeroi(HeroiDTO heroi);
        public List<Heroi> ListarHerois();
        public bool ExcluirHeroi(int idHeroi); // tratar retorno com fluentResult
         public List<Superpoderes> ListarPoderes();
    }
}
