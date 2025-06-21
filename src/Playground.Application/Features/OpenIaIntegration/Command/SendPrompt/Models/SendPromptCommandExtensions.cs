namespace Playground.Application.Features.OpenIaIntegration.Command.SendPrompt.Models
{
    public static class SendPromptCommandExtensions
    {
        public static string ToWarning(this SendPromptCommand input)
        {
            return $"{nameof(input.Prompt)}:{input.Prompt}|{nameof(input.Model)}:{input.Model}|{nameof(input.Temperature)}:{input.Temperature}|{nameof(input.FormattedErrosList)}:{input.FormattedErrosList()}";
        }

        public static string ToInformation(this SendPromptCommand input)
        {
            return $"{nameof(input.Prompt)}:{input.Prompt}|{nameof(input.Model)}:{input.Model}|{nameof(input.Temperature)}:{input.Temperature}";
        }
    }
}
