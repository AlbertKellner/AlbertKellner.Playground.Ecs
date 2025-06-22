using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Playground.API;
using Playground.Application.Features.Pokemon.GetByName.Models;
using Playground.Application.Infrastructure.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;

namespace Playground.IntegrationTests;

public class PokemonControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly ExternalApiOptions _options;

    public PokemonControllerIntegrationTest(WebApplicationFactory<Program> factory)
    {
        var customFactory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.Configure<Microsoft.AspNetCore.HttpsPolicy.HttpsRedirectionOptions>(o => o.HttpsPort = 0);
            });
        });

        _client = customFactory.CreateClient();

        _options = customFactory.Services.GetRequiredService<ExternalApiOptions>();
        _options.PokemonApi.Url = _client.BaseAddress!.ToString();
    }

    [Fact(DisplayName = "GetByNameExternalAsync QuandoPikachu DeveRetornarOk")]
    public async Task GetByNameExternalAsync_QuandoPikachu_DeveRetornarOk()
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "/pokemon/external-name/pikachu");
        request.Headers.Add("CorrelationId", Guid.NewGuid().ToString());
        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<GetByNamePokemonOutput>();
        Assert.NotNull(result);
        Assert.Equal("pikachu", result!.Name);
        Assert.Equal(112, result.BaseExperience);
        Assert.Equal("Grass", result.LocationAreaEncounters);
    }

    [Fact(DisplayName = "GetByNameExternalAsync QuandoNaoForPikachu DeveRetornarNoContent")]
    public async Task GetByNameExternalAsync_QuandoNaoForPikachu_DeveRetornarNoContent()
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "/pokemon/external-name/mew");
        request.Headers.Add("CorrelationId", Guid.NewGuid().ToString());
        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}
