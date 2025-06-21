namespace Playground.Application.Infrastructure.Configuration
{
    public class ExternalApiOptions
    {
        public PokemonApiOptions PokemonApi { get; set; } = new();
        public OpenIaApiOptions OpenIaApi { get; set; } = new();
    }

    public class PokemonApiOptions
    {
        public string Url { get; set; } = string.Empty;
        public TimeSpan Timeout { get; set; }
        public int RetryCount { get; set; }
        public int SleepDuration { get; set; }
    }

    public class OpenIaApiOptions
    {
        public string Url { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
    }
}
