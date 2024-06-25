﻿using Boosts;

namespace Platformer.Mechanics
{
    public interface IBoostApply
    {
        void ApplyBoost(AbilityType abilityType, float multiplier);
    }
}