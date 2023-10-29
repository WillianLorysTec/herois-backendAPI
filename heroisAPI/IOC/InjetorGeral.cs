using IOC.Modulos;
using Microsoft.Extensions.DependencyInjection;

namespace IOC
{
    public static class InjetorGeral
    {
        public static void RegistrarInjecoes(this IServiceCollection services)
        {
            FluxoGeralInjecao.Registrar(services);
        }
    }
}
