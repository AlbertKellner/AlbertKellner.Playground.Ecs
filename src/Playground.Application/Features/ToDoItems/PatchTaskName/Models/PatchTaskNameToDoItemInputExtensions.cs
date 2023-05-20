﻿using Playground.Application.Features.ToDoItems.PatchTaskName.Models;

namespace Playground.Application.Features.ToDoItems.Create.Models
{
    public static class PatchTaskNameToDoItemInputExtensions
    {
        public static string ToWarning(this PatchTaskNameToDoItemInput input)
        {
            return $@"{nameof(input.Id)}:{input.Id}|{nameof(input.Task)}:{input.Task}|{nameof(input.FormattedErrosList)}:{input.FormattedErrosList()}";
        }

        public static string ToError(this PatchTaskNameToDoItemInput input)
        {
            return $@"{nameof(input.Id)}:{input.Id}|{nameof(input.Task)}:{input.Task}";
        }
    }
}
