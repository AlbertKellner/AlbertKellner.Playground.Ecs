namespace Playground.Application.Shared.AsyncLocals
{
    public static class UserAuthorizationContext
    {
        private static readonly AsyncLocal<string> _userId = new();

        public static string GetUserId()
        {
            return _userId.Value ?? string.Empty;
        }

        public static void SetUserId(string userId)
        {
            _userId.Value = userId;
        }
    }
}
