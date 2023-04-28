using Microsoft.AspNetCore.Mvc;
using Playground.MinimalApi.Models;

namespace Playground.MinimalApi.EndPoints
{
    public static partial class EndpointMapper
    {
        public static WebApplication MinimalToDoListEndpoints(this WebApplication app)
        {
            var toDoItems = new List<ToDoItem>();

            // não é possivel
            //app.MapPost("/todoitems", (ToDoItem newItem,
            //    [FromHeader(Name = "token")] string userToken,
            //    [FromHeader(Name = "api-version")] string apiVersion = "1.0") =>

            app.MapPost("/todoitems", (
                [FromBody] ToDoItem newItem,
                [FromHeader(Name = "api-version")] string apiVersion, 
                [FromHeader(Name = "token")] string userToken) =>
            {
                int nextId = (toDoItems.Count > 0 ? toDoItems.Max(x => x.Id) : 0) + 1;
                newItem.Id = nextId;

                toDoItems.Add(newItem);

                return Results.Created($"/todoitems/{newItem.Id}", newItem);
            });

            app.MapGet("/todoitems", (
                [FromHeader(Name = "api-version")] string apiVersion, 
                [FromHeader(Name = "token")] string usertoken) =>
            {
                return Results.Ok(toDoItems);
            });

            app.MapGet("/todoitems/{id}", (
                [FromRoute] int id,
                [FromHeader(Name = "api-version")] string apiVersion,
                [FromHeader(Name = "token")] string usertoken) =>
            {
                var item = toDoItems.FirstOrDefault(x => x.Id == id);

                if (item == null)
                {
                    return Results.NoContent();
                }

                return Results.Ok(item);
            });

            app.MapPut("/todoitems/{id}", (
                [FromRoute] int id, 
                [FromBody] ToDoItem updatedItem,
                [FromHeader(Name = "api-version")] string apiVersion,
                [FromHeader(Name = "token")] string usertoken) =>
            {
                var index = toDoItems.FindIndex(x => x.Id == id);

                if (index == -1)
                {
                    return Results.NotFound();
                }

                updatedItem.Id = id;

                toDoItems[index] = updatedItem;

                return Results.Ok();
            });

            app.MapPatch("/todoitems/{id}/completed", (
                [FromRoute] int id, 
                [FromBody] bool isCompleted,
                [FromHeader(Name = "api-version")] string apiVersion,
                [FromHeader(Name = "token")] string usertoken) =>
            {
                var item = toDoItems.FirstOrDefault(x => x.Id == id);

                if (item == null)
                {
                    return Results.NotFound();
                }

                item.IsCompleted = isCompleted;

                return Results.Ok(item);
            });

            app.MapDelete("/todoitems/{id}", (
                [FromRoute] int id,
                [FromHeader(Name = "api-version")] string apiVersion,
                [FromHeader(Name = "token")] string usertoken) =>
            {
                var item = toDoItems.FirstOrDefault(x => x.Id == id);

                if (item == null)
                {
                    return Results.NotFound();
                }

                toDoItems.Remove(item);

                return Results.Ok();
            });

            return app;
        }
    }

}
