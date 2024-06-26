using UnityEngine;

namespace UserInput
{
    public interface IDirectionReader
    {
        Vector2 Direction { get; }
    }
}