using System.Collections.Generic;

namespace MvcIng.Logic
{
    public enum SessionKeys
    {
        Permissions,
        LogonData,
        CurrentSkin,
        CompareObject,
        UserID,
        FirstName,
        LastName
    }

    public class SessionBag : ISessionBag<SessionKeys>
    {
        private readonly Dictionary<string, object> _Data = new Dictionary<string, object>();

        public T Get<T>(SessionKeys key)
        {
            return Get<T>(key.ToString());
                   
        }

        public T Get<T>(string key)
        {
            if (!_Data.ContainsKey(key))
            {
                return default(T);
            }

            return (T)_Data[key];
        }

        public bool Exists(string key)
        {
            return _Data.ContainsKey(key);
        }

        public bool Exists(SessionKeys key)
        {
            return Exists(key.ToString());
        }

        public void Set<T>(SessionKeys key, T value)
        {
            Set(key.ToString(), value);
        }

        public void Set<T>(string key, T value)
        {
            if (_Data.ContainsKey(key))
            {
                _Data[key] = value;
            }

            else
            {
                _Data.Add(key, value);
            }
        }

        public void Delete(SessionKeys key)
        {
            Delete(key.ToString());
        }

        public void Delete(string key)
        {
            if (_Data.ContainsKey(key))
            {
                _Data.Remove(key);
            }
        }
    }
}
