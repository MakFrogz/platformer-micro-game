using System;
using UnityEngine;

namespace Platformer.Mechanics
{
    public interface ICollision
    {
        Bounds Bounds { get; }
        void Bounce(float value);
    }
}