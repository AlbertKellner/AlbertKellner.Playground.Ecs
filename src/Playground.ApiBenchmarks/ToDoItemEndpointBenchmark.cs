using BenchmarkDotNet.Attributes;
using Microsoft.AspNetCore.Mvc.Testing;
using Playground.API;

namespace Playground.ApiBenchmarks;

[MemoryDiagnoser]
public class ToDoItemEndpointBenchmark : IDisposable
{
    private WebApplicationFactory<Program> _factory = null!;
    private HttpClient _client = null!;

    [GlobalSetup]
    public void Setup()
    {
        _factory = new WebApplicationFactory<Program>();
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Benchmark(Description = "GET /todo/1")]
    public async Task GetByIdAsync()
    {
        using var response = await SendAsync("/todo/1");
        await response.Content.ReadAsStringAsync();
    }

    [Benchmark(Description = "GET /todo")]
    public async Task GetAllAsync()
    {
        using var response = await SendAsync("/todo");
        await response.Content.ReadAsStringAsync();
    }

    private async Task<HttpResponseMessage> SendAsync(string uri)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, uri);
        request.Headers.Add("CorrelationId", Guid.NewGuid().ToString());
        return await _client.SendAsync(request);
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    public void Dispose()
    {
        _client?.Dispose();
        _factory?.Dispose();
    }
}
