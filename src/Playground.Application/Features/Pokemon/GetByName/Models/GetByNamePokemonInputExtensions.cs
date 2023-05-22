namespace Playground.Application.Features.Pokemon.GetByName.Models
{
    public static class GetByNamePokemonInputExtensions
    {
        public static string ToWarning(this GetByNamePokemonInput input)
        {
            return $@"{nameof(input.Name)}:{input.Name}|{nameof(input.FormattedErrosList)}:{input.FormattedErrosList()}";
        }

        public static string ToError(this GetByNamePokemonInput input)
        {
            return $@"{nameof(input.Name)}:{input.Name}";
        }

        public static string ToInformation(this GetByNamePokemonInput input)
        {
            return $@"{nameof(input.Name)}:{input.Name}";
        }
    }
}
