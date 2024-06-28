using System.Collections.Generic;
using Mechanics.Player;
using UnityEngine;
using Utils;
using VContainer;

namespace Boosts
{
    public class BoostHandler : MonoBehaviour, IBoostHandler
    {
        private IBoostApply _player;
        private Dictionary<AbilityType, CountdownTimer> _timers;

        [Inject]
        private void Construct(IBoostApply player)
        {
            _player = player;
        }
        
        private void Awake()
        {
            _timers = new Dictionary<AbilityType, CountdownTimer>();
        }

        public void ApplyBoost(BoostData boostData)
        {
            if (!_timers.TryGetValue(boostData.AbilityType, out CountdownTimer timer))
            {
                timer = CreateTimer(boostData.Duration);
                _timers.Add(boostData.AbilityType, timer);
            }
            
            if (timer.IsRunning)
            {
                return;
            }
            
            timer.Reset();
            _player.ApplyBoost(boostData.AbilityType, boostData.AbilityMultiplier);
            timer.Start();
        }

        private void Update()
        {
            foreach (var pair in _timers)
            {
                pair.Value.Tick(Time.deltaTime);
                
                if (pair.Value.IsRunning)
                {
                    continue;
                }
                
                _player.ApplyBoost(pair.Key, 1f);
            }
        }

        private CountdownTimer CreateTimer(float time)
        {
            return new CountdownTimer(time);
        }
    }
}