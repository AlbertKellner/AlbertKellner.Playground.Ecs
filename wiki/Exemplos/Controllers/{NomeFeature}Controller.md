# Exemplo de {NomeFeature}Controller

Modelo de referência para criação de novos controllers e actions. Substitua `{NomeFeature}` pelo nome da sua funcionalidade.

```csharp
[ApiController]
[Route("{nomeFeature}")]
public class {NomeFeature}Controller : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> {NomeFeature}GetAllAsync(CancellationToken cancellationToken)
    {
        // TODO: recuperar todos os itens
        return Ok(new[] { "todo" });
    }

    [HttpGet("{id:long}", Name = "{NomeFeature}GetById")]
    public async Task<IActionResult> {NomeFeature}GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        // TODO: recuperar item por id
        return Ok(new { Id = id });
    }

    [HttpPost]
    public async Task<IActionResult> {NomeFeature}CreateAsync([FromBody] {NomeFeature}Input input, CancellationToken cancellationToken)
    {
        // TODO: criar item
        return CreatedAtRoute("{NomeFeature}GetById", new { id = 1 }, input);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> {NomeFeature}UpdateAsync(long id, [FromBody] {NomeFeature}Input input, CancellationToken cancellationToken)
    {
        // TODO: atualizar item
        return NoContent();
    }

    [HttpPatch("{id:long}/task-name/{taskName}")]
    public async Task<IActionResult> {NomeFeature}PatchTaskNameAsync(long id, string taskName, CancellationToken cancellationToken)
    {
        // TODO: atualizar parcialmente o nome da tarefa
        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> {NomeFeature}DeleteAsync(long id, CancellationToken cancellationToken)
    {
        // TODO: remover item
        return NoContent();
    }
}
```

Esses exemplos servem apenas como referência e devem ser adaptados ao criar novos arquivos.
