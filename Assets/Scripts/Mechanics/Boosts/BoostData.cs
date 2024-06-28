using UnityEngine;

namespace Boosts
{
    [CreateAssetMenu(fileName = "BoostData", menuName = "ScriptableObjects/BoostData")]
    public class BoostData : ScriptableObject
    {
        [SerializeField] private AbilityType _abilityType;
        [SerializeField][Min(1)] private float _abilityMultiplier;
        [SerializeField] private float _duration;

        public AbilityType AbilityType => _abilityType;
        public float AbilityMultiplier => _abilityMultiplier;
        public float Duration => _duration;
    }
}