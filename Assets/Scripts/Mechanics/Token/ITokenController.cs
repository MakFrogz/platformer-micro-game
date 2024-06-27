namespace Platformer.Mechanics
{
    public interface ITokenController
    {
        bool IsCongratulationsTokenCount { get; }
        void Reset();
    }
}