using Moq;
using Moq.Dapper;
using Dapper;
using Playground.Application.Features.ToDoItems.Command.Create.Repositories;
using Playground.Application.Features.ToDoItems.Command.Create.Models;
using System.Data;

namespace Playground.Tests.Controllers
{
    public class CreateTodoItemRepositoryTest
    {
        private readonly Mock<IDbConnection> _mockConnection;
        private readonly CreateTodoItemRepository _repository;

        public CreateTodoItemRepositoryTest()
        {
            _mockConnection = new Mock<IDbConnection>();
            _repository = new CreateTodoItemRepository(_mockConnection.Object);
        }

        [Fact]
        public async Task CreateToDoItemAsync_DeveExecutarQuery()
        {
            var command = new CreateToDoItemCommand { Task = "task" };
            var expected = new CreateToDoItemOutput { Id = 1, Task = "task", IsCompleted = false };

            _mockConnection.SetupDapperAsync(c => c.QueryFirstOrDefaultAsync<CreateToDoItemOutput>(It.IsAny<CommandDefinition>()))
                .ReturnsAsync(expected);

            var result = await _repository.CreateToDoItemAsync(command, CancellationToken.None);

            Assert.Equal(expected.Id, result.Id);
            Assert.Equal(expected.Task, result.Task);
            Assert.Equal(expected.IsCompleted, result.IsCompleted);
        }
    }
}
