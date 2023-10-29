using Interfaces.Repositorio;
using Interfaces.Servico;
using Microsoft.Extensions.DependencyInjection;
using Repositorio;
using Servico;

namespace IOC.Modulos
{
    public static class FluxoGeralInjecao
    {
        public static void Registrar(IServiceCollection serviceCollection)
        {
            RegistrarServicos(serviceCollection);
            RegistrarRepositorios(serviceCollection);
        }

        private static void RegistrarServicos(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IFluxoHeroiServico, FluxoHeroiServico>();
        }

        private static void RegistrarRepositorios(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IFluxoHeroiRepositorio, FluxoHeroiRepositorio>();
        }
    }
}
