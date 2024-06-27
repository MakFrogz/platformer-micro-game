using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "PlatformerSettings", menuName = "ScriptableObjects/PlatformerSettings")]
    public class PlatformerSettings : ScriptableObject
    {
        [SerializeField] private float _jumpModifier = 1.5f;
        [SerializeField] private float _jumpDeceleration = 0.5f;

        public float JumpModifier => _jumpModifier;
        public float JumpDeceleration => _jumpDeceleration;
    }
}