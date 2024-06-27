using System;

namespace SaveLoad
{
    [Serializable]
    public class PlayerData
    {
        public int Health;
        public int Tokens;
        public int Distance;

        public PlayerData(int health, int tokens, int distance)
        {
            Health = health;
            Tokens = tokens;
            Distance = distance;
        }
    }
}