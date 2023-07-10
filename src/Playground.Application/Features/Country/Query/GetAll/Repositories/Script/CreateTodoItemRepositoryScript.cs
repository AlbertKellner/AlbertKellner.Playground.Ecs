namespace Playground.Application.Features.Country.Command.Create.Repositories.Script
{
    internal static class GetAllCountryRepositoryScript
    {
        internal const string SqlScript =
            @"
                select * from country
            ";
    }
}
