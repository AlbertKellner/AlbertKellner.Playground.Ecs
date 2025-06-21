namespace Playground.Application.Features.OpenIaIntegration.Command.SendPrompt.Models
{
    public class SendPromptOutput
    {
        public string Response { get; set; } = string.Empty;

        public bool IsValid() => !string.IsNullOrWhiteSpace(Response);
    }
}
