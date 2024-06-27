using UnityEngine;

namespace Mechanics.Player
{
    public interface IPlayerMovement
    {
        Vector2 Velocity { get; }
        void ControlEnable();
        void ControlDisable();
        void SetDirection(Vector2 direction);
    }
}