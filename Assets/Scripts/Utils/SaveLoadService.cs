using UnityEngine;

namespace Utils
{
    public class SaveLoadService : ISaveLoadService
    {
        public void Save<T>(string key, T data)
        {
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
        }

        public T Load<T>(string key, T defaultData = default)
        {
            string json = PlayerPrefs.GetString(key, string.Empty);
            if (string.IsNullOrEmpty(json))
            {
                return defaultData;
            }

            return JsonUtility.FromJson<T>(json);
        }
        
        public bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        public void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        public void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}