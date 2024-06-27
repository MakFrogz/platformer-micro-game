using Boosts;

namespace Mechanics.Player
{
    public interface IBoostApply
    {
        void ApplyBoost(AbilityType abilityType, float multiplier);
    }
}