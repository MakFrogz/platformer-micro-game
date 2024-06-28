using System;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class PlayerModel : IHealthModel, ITokenModel, IDistanceModel, IPlayerModel
    {
        public event Action<int> OnHealthChangedEvent; 
        public event Action<int> OnTokensChangedEvent; 
        public event Action<int> OnDistanceChangedEvent; 

        private int _health;
        private int _tokens;
        private int _distance;

        public int Health => _health;
        public int Tokens => _tokens;
        public int Distance => _distance;
        public bool IsAlive => _health > 0;

        public void SetHealth(int health)
        {
            _health = Mathf.Max(0, health);
            OnHealthChangedEvent?.Invoke(_health);
        }

        public void DecreaseHealth()
        {
            _health = Mathf.Max(0, _health - 1);
            OnHealthChangedEvent?.Invoke(_health);
        }
        
        public void SetTokens(int coins)
        {
            _tokens = Mathf.Max(0, coins);
            OnTokensChangedEvent?.Invoke(_tokens);
        }

        public void AddToken()
        {
            _tokens += 1;
            OnTokensChangedEvent?.Invoke(_tokens);
        }
        
        public void SetDistance(int distance)
        {
            _distance = Mathf.Max(0, distance);
            OnDistanceChangedEvent?.Invoke(_distance);
        }
    }
}