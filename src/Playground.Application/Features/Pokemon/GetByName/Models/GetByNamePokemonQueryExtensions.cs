namespace Playground.Application.Features.Pokemon.GetByName.Models
{
    public static class GetByNamePokemonQueryExtensions
    {
        public static string ToWarning(this GetByNamePokemonQuery input)
        {
            return $@"{nameof(input.Name)}:{input.Name}|{nameof(input.FormattedErrosList)}:{input.FormattedErrosList()}";
        }

        public static string ToError(this GetByNamePokemonQuery input)
        {
            return $@"{nameof(input.Name)}:{input.Name}";
        }

        public static string ToInformation(this GetByNamePokemonQuery input)
        {
            return $@"{nameof(input.Name)}:{input.Name}";
        }
    }
}
