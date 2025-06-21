using Autofac;
using Playground.Application.Shared.ExternalServices.Interfaces;
using Playground.Application.Shared.ExternalServices;

namespace Playground.Application.Shared.AutofacModules
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PokemonApi>().As<IPokemonApi>();
            builder.RegisterType<OpenAiApi>().As<IOpenAiApi>();
        }
    }
}
