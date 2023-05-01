using Autofac;
using MediatR;
using Playground.Application.Features.ToDoItems.Create.Models;
using Playground.Application.Features.ToDoItems.Create.UseCase;
using Playground.Application.Features.ToDoItems.Delete.Models;
using Playground.Application.Features.ToDoItems.Delete.UseCase;
using Playground.Application.Features.ToDoItems.GetAll.Models;
using Playground.Application.Features.ToDoItems.GetAll.UseCase;
using Playground.Application.Features.ToDoItems.GetById.Models;
using Playground.Application.Features.ToDoItems.GetById.UseCase;
using Playground.Application.Features.ToDoItems.IsCompleted.Models;
using Playground.Application.Features.ToDoItems.IsCompleted.UseCase;
using Playground.Application.Features.ToDoItems.PatchTaskName.Models;
using Playground.Application.Features.ToDoItems.PatchTaskName.UseCase;
using Playground.Application.Features.ToDoItems.Update.Models;
using Playground.Application.Features.ToDoItems.Update.UseCase;
using System.Reflection;

namespace Playground.Application.Shared.AutofacModules
{
    public class HandlersModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(CreateToDoItemInput).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(CreateToDoItemUseCaseHandler).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<>));

            builder.RegisterAssemblyTypes(typeof(GetByIdToDoItemInput).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(GetByIdToDoItemUseCaseHandler).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<>));

            builder.RegisterAssemblyTypes(typeof(GetAllToDoItemInput).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(GetAllToDoItemUseCaseHandler).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<>));

            builder.RegisterAssemblyTypes(typeof(UpdateToDoItemInput).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(UpdateToDoItemUseCaseHandler).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<>));

            builder.RegisterAssemblyTypes(typeof(DeleteToDoItemInput).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(DeleteToDoItemUseCaseHandler).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<>));

            builder.RegisterAssemblyTypes(typeof(PatchTaskNameToDoItemInput).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(PatchTaskNameToDoItemUseCaseHandler).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<>));

            builder.RegisterAssemblyTypes(typeof(IsCompletedToDoItemInput).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(IsCompletedToDoItemUseCaseHandler).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<>));
        }
    }
}
