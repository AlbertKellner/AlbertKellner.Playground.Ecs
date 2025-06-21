using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Playground.API;
using Playground.Application.Features.Pokemon.GetByName.Models;
using Playground.Application.Shared.Domain.ApiDto;
using Playground.Application.Shared.ExternalServices.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Autofac;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System;
using System.Threading.Tasks;

namespace Playground.IntegrationTests;

public class PokemonControllerMockedIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public PokemonControllerMockedIntegrationTest(WebApplicationFactory<Program> factory)
    {
        var customFactory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll<IPokemonApi>();
                services.AddSingleton<IPokemonApi, FakePokemonApi>();
            });
        });

        _client = customFactory.CreateClient();
    }

    [Fact(DisplayName = "GetByNameInternalAsync QuandoPikachu DeveRetornarOk")]
    public async Task GetByNameInternalAsync_QuandoPikachu_DeveRetornarOk()
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "/pokemon/internal-name/pikachu");
        request.Headers.Add("CorrelationId", Guid.NewGuid().ToString());
        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<PokemonOutApiDto>();
        Assert.NotNull(result);
        Assert.Equal("pikachu", result!.Name);
        Assert.Equal(112, result.BaseExperience);
        Assert.Equal("Grass", result.LocationAreaEncounters);
    }

    [Fact(DisplayName = "GetByNameInternalAsync QuandoNaoForPikachu DeveRetornarNoContent")]
    public async Task GetByNameInternalAsync_QuandoNaoForPikachu_DeveRetornarNoContent()
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "/pokemon/internal-name/mew");
        request.Headers.Add("CorrelationId", Guid.NewGuid().ToString());
        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

}

internal class FakePokemonApi : IPokemonApi
{
    public Task<PokemonOutApiDto> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        if (name == "pikachu")
        {
            return Task.FromResult(new PokemonOutApiDto
            {
                Name = "pikachu",
                BaseExperience = 112,
                LocationAreaEncounters = "Grass"
            });
        }

        return Task.FromResult(new PokemonOutApiDto());
    }
}
