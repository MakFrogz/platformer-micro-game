namespace Model
{
    public interface IHealthModel
    {
        public int Health { get; }
        public bool IsAlive { get; }
        public void SetHealth(int health);
        public void DecreaseHealth();
    }
}