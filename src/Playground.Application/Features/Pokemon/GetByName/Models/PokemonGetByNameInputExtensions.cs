namespace Playground.Application.Features.Pokemon.GetByName.Models
{
    public static class PokemonGetByNameInputExtensions
    {
        public static string ToWarning(this PokemonGetByNameInput input)
        {
            return $@"{nameof(input.Name)}:{input.Name}|{nameof(input.FormattedErrosList)}:{input.FormattedErrosList()}";
        }

        public static string ToError(this PokemonGetByNameInput input)
        {
            return $@"{nameof(input.Name)}:{input.Name}";
        }
    }
}
