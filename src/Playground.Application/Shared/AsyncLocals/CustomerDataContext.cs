namespace Playground.Application.Shared.AsyncLocals
{
    public class CustomerData
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public static class CustomerDataContext
    {
        private static readonly AsyncLocal<CustomerData> _customerData = new();

        public static string GetId()
        {
            return _customerData.Value?.Id ?? string.Empty;
        }

        public static string GetName()
        {
            return _customerData.Value?.Name ?? string.Empty;
        }

        public static void SetId(string id)
        {
            if (_customerData.Value != null)
            {
                _customerData.Value.Id = id;
            }
        }

        public static void SetName(string name)
        {
            if (_customerData.Value != null)
            {
                _customerData.Value.Name = name;
            }
        }
    }
}
