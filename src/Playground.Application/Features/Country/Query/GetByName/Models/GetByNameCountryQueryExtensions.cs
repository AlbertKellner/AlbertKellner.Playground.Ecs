namespace Playground.Application.Features.Country.Query.GetByName.Models
{
    public static class GetByNameCountryQueryExtensions
    {
        public static string ToWarning(this GetByNameCountryQuery input)
        {
            return $"{nameof(input.Name)}:{input.Name}|{nameof(input.FormattedErrosList)}:{input.FormattedErrosList()}";
        }

        public static string ToInformation(this GetByNameCountryQuery input)
        {
            return $"{nameof(input.Name)}:{input.Name}";
        }
    }
}
