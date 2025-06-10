using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Playground.Application.Features.Country.Query.GetAll.Models;
using Playground.Application.Features.Country.Query.GetAll.UseCase;
using Playground.Application.Features.Country.Query.GetByName.Models;
using Playground.Application.Features.Country.Query.GetByName.UseCase;
using Playground.Application.Features.Pokemon.GetByName.Models;
using Playground.Application.Features.Pokemon.GetByName.UseCase;
using Playground.Application.Features.ToDoItems.Command.Create.Models;
using Playground.Application.Features.ToDoItems.Command.Create.UseCase;
using Playground.Application.Features.ToDoItems.Command.Delete.Models;
using Playground.Application.Features.ToDoItems.Command.Delete.UseCase;
using Playground.Application.Features.ToDoItems.Command.PatchIsCompleted.Models;
using Playground.Application.Features.ToDoItems.Command.PatchIsCompleted.UseCase;
using Playground.Application.Features.ToDoItems.Command.PatchTaskName.Models;
using Playground.Application.Features.ToDoItems.Command.PatchTaskName.UseCase;
using Playground.Application.Features.ToDoItems.Command.Update.Models;
using Playground.Application.Features.ToDoItems.Command.Update.UseCase;
using Playground.Application.Features.ToDoItems.Query.GetAll.Models;
using Playground.Application.Features.ToDoItems.Query.GetAll.UseCase;
using Playground.Application.Features.ToDoItems.Query.GetById.Models;
using Playground.Application.Features.ToDoItems.Query.GetById.UseCase;

namespace Playground.Application.Shared.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRequestHandlers(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<CreateToDoItemCommand, CreateToDoItemOutput>, CreateToDoItemUseCaseHandler>();
            services.AddTransient<IRequestHandler<GetByIdToDoItemQuery, GetByIdToDoItemOutput>, GetByIdToDoItemUseCaseHandler>();
            services.AddTransient<IRequestHandler<GetAllToDoItemQuery, IEnumerable<GetAllToDoItemOutput>>, GetAllToDoItemUseCaseHandler>();
            services.AddTransient<IRequestHandler<UpdateToDoItemCommand, UpdateToDoItemOutput>, UpdateToDoItemUseCaseHandler>();
            services.AddTransient<IRequestHandler<DeleteToDoItemCommand, DeleteToDoItemOutput>, DeleteToDoItemUseCaseHandler>();
            services.AddTransient<IRequestHandler<PatchTaskNameToDoItemCommand, PatchTaskNameToDoItemOutput>, PatchTaskNameToDoItemUseCaseHandler>();
            services.AddTransient<IRequestHandler<IsCompletedToDoItemCommand, IsCompletedToDoItemOutput>, IsCompletedToDoItemUseCaseHandler>();
            services.AddTransient<IRequestHandler<GetByNamePokemonQuery, GetByNamePokemonOutput>, GetByNamePokemonUseCaseHandler>();
            services.AddTransient<IRequestHandler<GetByNameCountryQuery, GetByNameCountryOutput>, GetByNameCountryUseCaseHandler>();
            services.AddTransient<IRequestHandler<GetAllCountryQuery, IEnumerable<GetAllCountryOutput>>, GetAllCountryUseCaseHandler>();
            return services;
        }
    }
}
