namespace Platformer.UI.PlayerStats
{
    public interface IDistanceModel
    {
        int Distance { get; }
        void SetDistance(int distance);
    }
}