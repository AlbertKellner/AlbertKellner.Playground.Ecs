using Microsoft.AspNetCore.Mvc.Testing;
using Playground.API;
using Playground.Application.Features.ToDoItems.Query.GetById.Models;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Playground.IntegrationTests;

public class ToDoItemControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    private static async Task<(HttpResponseMessage Response, string Logs)>
        SendAsync(HttpClient client, HttpRequestMessage request)
    {
        var original = Console.Out;
        using var writer = new StringWriter();
        Console.SetOut(writer);
        try
        {
            var response = await client.SendAsync(request);
            return (response, writer.ToString());
        }
        finally
        {
            Console.SetOut(original);
        }
    }

    public ToDoItemControllerIntegrationTest(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact(DisplayName = "GetByIdAsync QuandoIdExiste DeveRetornarItem")]
    public async Task GetByIdAsync_QuandoIdExiste_DeveRetornarItem()
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "/todo/99");
        request.Headers.Add("CorrelationId", Guid.NewGuid().ToString());
        var (response, logs) = await SendAsync(_client, request);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<GetByIdToDoItemOutput>();
        Assert.NotNull(result);
        Assert.Equal(99, result!.Id);
        Assert.Equal("GetById - ToDoItem - UseCaseHandler", result.Task);
        Assert.True(result.IsCompleted);

        Assert.DoesNotContain("[ERR]", logs);
    }

    [Fact(DisplayName = "GetByIdAsync QuandoIdNaoExiste DeveRetornarNoContent")]
    public async Task GetByIdAsync_QuandoIdNaoExiste_DeveRetornarNoContent()
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "/todo/100");
        request.Headers.Add("CorrelationId", Guid.NewGuid().ToString());
        var (response, logs) = await SendAsync(_client, request);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.DoesNotContain("[ERR]", logs);
    }

    [Fact(DisplayName = "GetByIdAsync QuandoIdInvalido DeveRetornarBadRequest")]
    public async Task GetByIdAsync_QuandoIdInvalido_DeveRetornarBadRequest()
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "/todo/0");
        request.Headers.Add("CorrelationId", Guid.NewGuid().ToString());
        var (response, logs) = await SendAsync(_client, request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var errors = await response.Content.ReadFromJsonAsync<string[]>();
        Assert.NotNull(errors);
        Assert.Contains("Id precisa ser maior que zero", errors!);
        Assert.DoesNotContain("[ERR]", logs);
    }
}
