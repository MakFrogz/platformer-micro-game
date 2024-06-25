using System;
using UnityEngine;

namespace UserInput
{
    public interface IInputReader
    {
        event Action<Vector2> OnMove;
        event Action OnStop;
    }
}