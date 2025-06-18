# Controllers and Endpoints

The following list shows each controller action with an example `curl` command.

## AuthController
`POST /Auth/generate-token`
```bash
curl -X POST "https://localhost:5001/Auth/generate-token" \
     -H "Content-Type: application/json" \
     -d '{"userId":"1","userName":"User","accessGroup":"ADM"}'
```
```csharp
[HttpPost("generate-token")]
public IActionResult GenerateToken([FromBody] AuthUser user)
{
    var tokenHandler = new JwtSecurityTokenHandler();

    var secretKey = GetUniqueKey(32);
    var key = Encoding.ASCII.GetBytes(secretKey);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new Claim[]
        {
            new Claim("UserId", user.UserId),
            new Claim("UserName", user.UserName),
            new Claim("AccessGroup", user.AccessGroup)
        }),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);

    _logger.LogInformation($"[AuthController][GenerateToken] Token gerado com sucesso");

    return Ok(new { Token = tokenHandler.WriteToken(token) });
}
```

## PokemonController
* `GET /pokemon/external-name/{name}`
  ```bash
  curl "https://localhost:5001/pokemon/external-name/pikachu"
  ```
  ```csharp
  [HttpGet("external-name/{name}")]
  [ResponseCache(CacheProfileName = ResponseCacheProfile.For1Second)]
  [ProducesResponseType((int)HttpStatusCode.NoContent)]
  [ProducesResponseType((int)HttpStatusCode.BadRequest)]
  [ProducesResponseType(typeof(GetByNamePokemonOutput), (int)HttpStatusCode.OK)]
  public async Task<IActionResult> GetByNameExternalAsync(
      [FromRoute] string name,
      [FromQuery] GetByNamePokemonQuery input,
      CancellationToken cancellationToken)
  {
      input.SetName(name);

      if (input.IsInvalid())
      {
          _logger.LogWarning("[PokemonController][GetByNameExternalAsync] Retornando API com erro de validação. input:({@input})", input.ToWarning());

          return BadRequest(input.ErrosList());
      }

      _logger.LogInformation("[PokemonController][GetByNameExternalAsync] Iniciando caso de uso. input:({@input})", input.ToInformation());

      var output = await _mediator.Send(input, cancellationToken);

      if (output.IsValid())
      {
          _logger.LogInformation("[PokemonController][GetByNameExternalAsync] Retornando API com sucesso. input:({@input})", input.ToInformation());

          return Ok(output);
      }

      _logger.LogInformation("[PokemonController][GetByNameExternalAsync] Retornando API sem dados. input:({@input})", input.ToInformation());

      return NoContent();
  }
  ```
* `GET /pokemon/internal-name/{name}`
  ```bash
  curl "https://localhost:5001/pokemon/internal-name/pikachu"
  ```
  ```csharp
  [HttpGet("internal-name/{name}")]
  [ProducesResponseType((int)HttpStatusCode.NoContent)]
  [ProducesResponseType(typeof(PokemonOutApiDto), (int)HttpStatusCode.OK)]
  public IActionResult GetByNameInternalAsync(
      [FromRoute] string name)
  {
      if (name == "pikachu")
      {
          _logger.LogInformation("[PokemonController][GetByNameInternalAsync] Retorno do endpoint internal com sucesso. input:({@pokemonName})", name);

          return Ok(
              new PokemonOutApiDto
              {
                  Name = "pikachu",
                  BaseExperience = 112,
                  LocationAreaEncounters = "Grass"
              });
      }

      _logger.LogInformation("[PokemonController][GetByNameInternalAsync] Retorno do endpoint internal sem dados. input:({@pokemonName})", name);

      return NoContent();
  }
  ```

## CancellationTokenDemoController
* `GET /cancellation-token-demo/with`
  ```bash
  curl "https://localhost:5001/cancellation-token-demo/with"
  ```
  ```csharp
  [HttpGet("with")]
  [ProducesResponseType((int)HttpStatusCode.NoContent)]
  [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
  public IActionResult WithCancellationTokenDemo(
      CancellationToken cancellationToken)
  {
      _logger.LogInformation($"[Api][CancellationTokenDemoController][WithCancellationTokenDemo][Start] Iniciando Execução");

      try
      {
          IxJAsync(cancellationToken);
      }
      catch (OperationCanceledException)
      {
          _logger.LogWarning($"[Api][CancellationTokenDemoController][WithCancellationTokenDemo][OperationCanceledException] Execução interrompida");

          return NoContent();
      }
      finally
      {
          GC.Collect();
      }

      _logger.LogInformation($"[Api][CancellationTokenDemoController][WithCancellationTokenDemo][Ok] Execução completa");

      return Ok("Operation Completed Successfully");
  }
  ```
* `GET /cancellation-token-demo/without`
  ```bash
  curl "https://localhost:5001/cancellation-token-demo/without"
  ```
  ```csharp
  [HttpGet("without")]
  [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
  public IActionResult WithoutCancellationTokenDemo()
  {
      _logger.LogInformation($"[Api][CancellationTokenDemoController][WithoutCancellationTokenDemo][Start] Iniciando Execução");

      try
      {
          IxJ();
      }
      finally
      {
          GC.Collect();
      }

      _logger.LogInformation($"[Api][CancellationTokenDemoController][WithoutCancellationTokenDemo][Ok] Execução completa");

      return Ok("Operation Completed Successfully");
  }
  ```

## ToDoItemController
* `POST /todo`
  ```bash
  curl -X POST "https://localhost:5001/todo" \
       -H "Content-Type: application/json" \
       -d '{"task":"Nova tarefa","is_completed":false}'
  ```
  ```csharp
  [HttpPost]
  [ProducesResponseType((int)HttpStatusCode.BadRequest)]
  [ProducesResponseType(typeof(CreateToDoItemOutput), (int)HttpStatusCode.Created)]
  public async Task<IActionResult> CreateAsync(
      [FromBody] CreateToDoItemCommand input,
      CancellationToken cancellationToken)
  {
      _logger.LogInformation($"[Api][ToDoItemController][CreateAsync][Start] input:({input.ToInformation()})");

      if (input.IsInvalid())
      {
          _logger.LogWarning($"[Api][ToDoItemController][CreateAsync][BadRequest] input:({input.ToWarning()})");
          return BadRequest(input.ErrosList());
      }

      var output = await _mediator.Send(input, cancellationToken);

      _logger.LogInformation($"[Api][ToDoItemController][CreateAsync][Created] input:({input.ToInformation()})");
      return CreatedAtRoute(
          routeName: "GetById",
          routeValues: new { id = output.Id },
          value: output);
  }
  ```
* `GET /todo/{id}`
  ```bash
  curl "https://localhost:5001/todo/1"
  ```
  ```csharp
  [HttpGet("{id:long}", Name = "GetById")]
  [ProducesResponseType((int)HttpStatusCode.NoContent)]
  [ProducesResponseType((int)HttpStatusCode.BadRequest)]
  [ProducesResponseType(typeof(GetByIdToDoItemOutput), (int)HttpStatusCode.OK)]
  public async Task<IActionResult> GetByIdAsync(
      [FromRoute] long id,
      [FromQuery] GetByIdToDoItemQuery input,
      CancellationToken cancellationToken)
  {
      _logger.LogInformation($"[Api][ToDoItemController][GetByIdAsync][Start] input:({input.ToInformation()})");

      input.SetId(id);

      if (input.IsInvalid())
      {
          _logger.LogWarning($"[Api][ToDoItemController][GetByIdAsync][BadRequest] input:({input.ToWarning()})");
          return BadRequest(input.ErrosList());
      }

      var output = await _mediator.Send(input, cancellationToken);

      if (output.IsValid())
      {
          _logger.LogInformation($"[Api][ToDoItemController][GetByIdAsync][Ok] input:({input.ToInformation()})");
          return Ok(output);
      }

      _logger.LogInformation($"[Api][ToDoItemController][GetByIdAsync][NoContent] input:({input.ToInformation()})");
      return NoContent();
  }
  ```
* `GET /todo`
  ```bash
  curl "https://localhost:5001/todo"
  ```
  ```csharp
  [HttpGet()]
  [ProducesResponseType((int)HttpStatusCode.NoContent)]
  [ProducesResponseType(typeof(GetAllToDoItemOutput), (int)HttpStatusCode.OK)]
  public async Task<IActionResult> GetAllAsync(
      CancellationToken cancellationToken)
  {
      _logger.LogInformation($"[Api][ToDoItemController][GetAllAsync][Start]");

      var output = await _mediator.Send(new GetAllToDoItemQuery(), cancellationToken);

      if (output.Any())
      {
          _logger.LogInformation($"[Api][ToDoItemController][GetAllAsync][Ok]");
          return Ok(output);
      }

      _logger.LogInformation($"[Api][ToDoItemController][GetAllAsync][NoContent]");
      return NoContent();
  }
  ```
* `PUT /todo/{id}`
  ```bash
  curl -X PUT "https://localhost:5001/todo/1" \
       -H "Content-Type: application/json" \
       -d '{"task":"Atualizada","is_completed":true}'
  ```
  ```csharp
  [HttpPut("{id:long}")]
  [ProducesResponseType((int)HttpStatusCode.OK)]
  [ProducesResponseType((int)HttpStatusCode.NoContent)]
  [ProducesResponseType((int)HttpStatusCode.BadRequest)]
  public async Task<IActionResult> UpdateAsync(
      [FromRoute] long id,
      [FromBody] UpdateToDoItemCommand input,
      CancellationToken cancellationToken)
  {
      _logger.LogInformation($"[Api][ToDoItemController][UpdateAsync][Start] input:({input.ToInformation()})");

      input.SetId(id);

      if (input.IsInvalid())
      {
          _logger.LogWarning($"[Api][ToDoItemController][UpdateAsync][BadRequest] input:({input.ToWarning()})");
          return BadRequest(input.ErrosList());
      }

      var output = await _mediator.Send(input, cancellationToken);

      if (output.IsValid())
      {
          _logger.LogInformation($"[Api][ToDoItemController][UpdateAsync][Ok] input:({input.ToInformation()})");
          return Ok();
      }

      _logger.LogInformation($"[Api][ToDoItemController][UpdateAsync][NoContent] input:({input.ToInformation()})");
      return NoContent();
  }
  ```
* `PATCH /todo/{id}/task-name/{taskName}`
  ```bash
  curl -X PATCH "https://localhost:5001/todo/1/task-name/Task" \
       -H "Content-Type: application/json" \
       -d '{}'
  ```
  ```csharp
  [HttpPatch("{id:long}/task-name/{taskName}")]
  [ProducesResponseType((int)HttpStatusCode.OK)]
  [ProducesResponseType((int)HttpStatusCode.NoContent)]
  [ProducesResponseType((int)HttpStatusCode.BadRequest)]
  public async Task<IActionResult> PatchTaskNameAsync(
      [FromRoute] long id,
      [FromRoute] string taskName,
      [FromRoute] PatchTaskNameToDoItemCommand input,
      CancellationToken cancellationToken)
  {
      _logger.LogInformation($"[Api][ToDoItemController][PatchTaskNameAsync][Start] input:({input.ToInformation()})");

      input.SetId(id);
      input.SetTaskName(taskName);

      if (input.IsInvalid())
      {
          _logger.LogWarning($"[Api][ToDoItemController][PatchTaskNameAsync][BadRequest] input:({input.ToWarning()})");
          return BadRequest(input.ErrosList());
      }

      var output = await _mediator.Send(input, cancellationToken);

      if (output.IsValid())
      {
          _logger.LogInformation($"[Api][ToDoItemController][PatchTaskNameAsync][Ok] input:({input.ToInformation()})");
          return Ok();
      }

      _logger.LogInformation($"[Api][ToDoItemController][PatchTaskNameAsync][NoContent] input:({input.ToInformation()})");
      return NoContent();
  }
  ```
* `PATCH /todo/{id}/is-completed/{isCompleted}`
  ```bash
  curl -X PATCH "https://localhost:5001/todo/1/is-completed/true"
  ```
  ```csharp
  [HttpPatch("{id:long}/is-completed/{isCompleted}")]
  [ProducesResponseType((int)HttpStatusCode.OK)]
  [ProducesResponseType((int)HttpStatusCode.NoContent)]
  [ProducesResponseType((int)HttpStatusCode.BadRequest)]
  public async Task<IActionResult> PatchIsCompletedAsync(
      [FromRoute] long id,
      [FromRoute] bool isCompleted,
      CancellationToken cancellationToken)
  {
      var input = new IsCompletedToDoItemCommand(); //TODO: Extrair para parametro

      _logger.LogInformation($"[Api][ToDoItemController][PatchIsCompletedAsync][Start] input:({input.ToInformation()})");

      input.SetId(id);
      input.SetIsCompleted(isCompleted);

      if (input.IsInvalid())
      {
          _logger.LogWarning($"[Api][ToDoItemController][PatchIsCompletedAsync][BadRequest] input:({input.ToWarning()})");
          return BadRequest(input.ErrosList());
      }

      var output = await _mediator.Send(input, cancellationToken);

      if (output.IsValid())
      {
          _logger.LogInformation($"[Api][ToDoItemController][PatchIsCompletedAsync][Ok] input:({input.ToInformation()})");
          return Ok();
      }

      _logger.LogInformation($"[Api][ToDoItemController][PatchIsCompletedAsync][NoContent] input:({input.ToInformation()})");
      return NoContent();
  }
  ```
* `DELETE /todo/{id}`
  ```bash
  curl -X DELETE "https://localhost:5001/todo/1"
  ```
  ```csharp
  [HttpDelete("{id:long}")]
  [ProducesResponseType((int)HttpStatusCode.OK)]
  [ProducesResponseType((int)HttpStatusCode.NoContent)]
  [ProducesResponseType((int)HttpStatusCode.BadRequest)]
  public async Task<IActionResult> DeleteAsync(
      [FromRoute] long id,
      CancellationToken cancellationToken)
  {
      var input = new DeleteToDoItemCommand(id); //TODO: Extrair para parametro

      _logger.LogInformation($"[Api][ToDoItemController][DeleteAsync][Start] input:({input.ToInformation()})");

      if (input.IsInvalid())
      {
          _logger.LogWarning($"[Api][ToDoItemController][DeleteAsync][BadRequest] input:({input.ToWarning()})");
          return BadRequest(input.ErrosList());
      }

      var output = await _mediator.Send(input, cancellationToken);

      if (output.IsValid())
      {
          _logger.LogInformation($"[Api][ToDoItemController][DeleteAsync][Ok] input:({input.ToInformation()})");
          return Ok();
      }

      _logger.LogInformation($"[Api][ToDoItemController][DeleteAsync][NoContent] input:({input.ToInformation()})");
      return NoContent();
  }
  ```

## ToDoItemsController (v2.0)
* `GET /todo/{id}`
  ```bash
  curl -H "X-Version: 2.0" "https://localhost:5001/todo/1"
  ```
  ```csharp
  [HttpGet("{id:long}", Name = "GetById")]
  [ProducesResponseType((int)HttpStatusCode.NoContent)]
  [ProducesResponseType((int)HttpStatusCode.BadRequest)]
  [ProducesResponseType(typeof(GetByIdToDoItemOutput), (int)HttpStatusCode.OK)]
  public async Task<IActionResult> GetByIdAsync(
      [FromRoute] long id,
      [FromQuery] GetByIdToDoItemQuery input,
      CancellationToken cancellationToken)
  {
      input.SetId(id);

      if (input.IsInvalid())
      {
          _logger.LogWarning($"[Api][ToDoItemController][GetByIdAsync][BadRequest] V2 Teste - input:({input.ToWarning()})");

          return BadRequest(input.ErrosList());
      }

      var output = await _mediator.Send(input, cancellationToken);

      if (output.IsValid())
      {
          _logger.LogWarning($"[Api][ToDoItemController][GetByIdAsync][Ok]");

          return Ok(output);
      }

      _logger.LogWarning($"[Api][ToDoItemController][GetByIdAsync][NoContent]");

      return NoContent();
  }
  ```
