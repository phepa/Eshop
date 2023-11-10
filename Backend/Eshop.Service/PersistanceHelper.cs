using Eshop.Shared.Helpers;

namespace Eshop.Service
{
    public class PersistanceHelper
    {
        private readonly Dictionary<Guid, string> _notificationDatabase;

        public PersistanceHelper()
        {
            if (_notificationDatabase.IsNull())
            {
                _notificationDatabase = new Dictionary<Guid, string>();
            }
        }

        public void AddNotificaton(Guid key, string message)
        {
            _notificationDatabase.Add(key, message);
        }

        public string? GetNotificaton()
        {
            KeyValuePair<Guid, string> firstMessage = _notificationDatabase.FirstOrDefault();
            if (firstMessage.NotNull())
            {
                _notificationDatabase.Remove(firstMessage.Key);
                return firstMessage.Value;
            }

            return null;
        }
    }
}
