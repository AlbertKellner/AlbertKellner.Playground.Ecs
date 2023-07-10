namespace Playground.Application.Features.Country.Query.GetByName.Models
{
    public static class GetByIdToDoItemQueryExtensions
    {
        public static string ToWarning(this GetByNameCountryQuery input)
        {
            return $@"{nameof(input.Id)}:{input.Id}|{nameof(input.FormattedErrosList)}:{input.FormattedErrosList()}";
        }

        public static string ToInformation(this GetByNameCountryQuery input)
        {
            return $@"{nameof(input.Id)}:{input.Id}";
        }
    }
}
