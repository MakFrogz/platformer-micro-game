namespace Model
{
    public interface IHealthModel
    {
        int Health { get; }
        bool IsAlive { get; }
        void SetHealth(int health);
        void DecreaseHealth();
    }
}