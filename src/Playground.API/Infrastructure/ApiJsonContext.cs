using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Playground;

public record HealthCheckEntry(string name, string status, string exception, string duration);
public record HealthCheckResponse(string status, IEnumerable<HealthCheckEntry> checks);
public record ErrorResponse(string error);

[JsonSerializable(typeof(HealthCheckResponse))]
[JsonSerializable(typeof(HealthCheckEntry))]
[JsonSerializable(typeof(ErrorResponse))]
internal partial class ApiJsonContext : JsonSerializerContext
{
}
