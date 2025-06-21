using Microsoft.AspNetCore.Mvc.Testing;
using Playground.API;
using Playground.Application.Features.ToDoItems.Query.GetById.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Playground.IntegrationTests;

public class ToDoItemControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ToDoItemControllerIntegrationTest(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact(DisplayName = "GetByIdAsync QuandoIdExiste DeveRetornarItem")]
    public async Task GetByIdAsync_QuandoIdExiste_DeveRetornarItem()
    {
        var response = await _client.GetAsync("/todo/99");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<GetByIdToDoItemOutput>();
        Assert.Equal(99, result!.Id);
    }
}
