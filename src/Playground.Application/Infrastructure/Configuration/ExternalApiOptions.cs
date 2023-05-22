namespace Playground.Application.Infrastructure.Configuration
{
    public class ExternalApiOptions
    {
        public string PokemonApiUrl { get; set; } = string.Empty;
        public TimeSpan PokemonApiTimeout { get; set; }
        public int PokemonApiRetryCount { get; set; }
        public int PokemonApiSleepDuration { get; set; }
    }
}
