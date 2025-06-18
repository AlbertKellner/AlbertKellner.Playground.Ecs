using Autofac;
using MediatR;
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

namespace Playground.Application.Shared.AutofacModules
{
    public class HandlersModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CreateToDoItemUseCaseHandler>()
                .As<IRequestHandler<CreateToDoItemCommand, CreateToDoItemOutput>>();

            builder.RegisterType<GetByIdToDoItemUseCaseHandler>()
                .As<IRequestHandler<GetByIdToDoItemQuery, GetByIdToDoItemOutput>>();

            builder.RegisterType<GetAllToDoItemUseCaseHandler>()
                .As<IRequestHandler<GetAllToDoItemQuery, IEnumerable<GetAllToDoItemOutput>>>();

            builder.RegisterType<UpdateToDoItemUseCaseHandler>()
                .As<IRequestHandler<UpdateToDoItemCommand, UpdateToDoItemOutput>>();

            builder.RegisterType<DeleteToDoItemUseCaseHandler>()
                .As<IRequestHandler<DeleteToDoItemCommand, DeleteToDoItemOutput>>();

            builder.RegisterType<PatchTaskNameToDoItemUseCaseHandler>()
                .As<IRequestHandler<PatchTaskNameToDoItemCommand, PatchTaskNameToDoItemOutput>>();

            builder.RegisterType<IsCompletedToDoItemUseCaseHandler>()
                .As<IRequestHandler<IsCompletedToDoItemCommand, IsCompletedToDoItemOutput>>();

            builder.RegisterType<GetByNamePokemonUseCaseHandler>()
                .As<IRequestHandler<GetByNamePokemonQuery, GetByNamePokemonOutput>>();

        }
    }
}
