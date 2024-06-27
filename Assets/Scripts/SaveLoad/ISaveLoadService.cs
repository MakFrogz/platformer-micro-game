namespace SaveLoad
{
    public interface ISaveLoadService
    {
        void Save<T>(string key, T data);
        T Load<T>(string key, T defaultData = default);
        bool HasKey(string key);
        void DeleteKey(string key);
        void DeleteAll();
    }
}