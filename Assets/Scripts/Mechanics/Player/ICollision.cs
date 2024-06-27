using UnityEngine;

namespace Mechanics.Player
{
    public interface ICollision
    {
        Bounds Bounds { get; }
        void Bounce(float value);
    }
}