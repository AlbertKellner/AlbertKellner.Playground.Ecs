using Dapper;
using System.Reflection;
using System.Text.Json.Serialization;
using Playground.Application.Features.Country.Query.GetAll.Models;
using Playground.Application.Features.Country.Query.GetByName.Models;
using Playground.Application.Features.ToDoItems.Command.Create.Models;

namespace Playground.Application.Infrastructure.Extensions
{
    internal static class DapperMappingExtensions
    {
        private static bool _configured;

        internal static void RegisterMappings()
        {
            if (_configured) return;
            _configured = true;

            Register<GetAllCountryOutput>();
            Register<GetByNameCountryOutput>();
            Register<CreateToDoItemOutput>();
        }

        private static void Register<T>()
        {
            SqlMapper.SetTypeMap(typeof(T), new CustomPropertyTypeMap(
                typeof(T),
                (type, columnName) => type.GetProperties()
                    .FirstOrDefault(prop =>
                        prop.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase) == true ||
                        prop.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase))!));
        }
    }
}
