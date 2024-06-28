using System;

namespace Model
{
    public interface IPlayerModel
    {
        event Action<int> OnHealthChangedEvent;
        event Action<int> OnTokensChangedEvent;
        event Action<int> OnDistanceChangedEvent;
        int Health { get; }
        int Tokens { get; }
        int Distance { get; }
        bool IsAlive { get; }
        void SetHealth(int health);
        void DecreaseHealth();
        void SetTokens(int coins);
        void AddToken();
        void SetDistance(int distance);
    }
}