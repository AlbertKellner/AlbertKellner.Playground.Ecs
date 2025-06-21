using Microsoft.AspNetCore.Mvc.Testing;
using Playground.API;
using Playground.Application.Features.ToDoItems.Query.GetById.Models;
using System.Net;
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
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<GetByIdToDoItemOutput>();
        Assert.NotNull(result);
        Assert.Equal(99, result!.Id);
        Assert.Equal("GetById - ToDoItem - UseCaseHandler", result.Task);
        Assert.True(result.IsCompleted);
    }
}
