using Audio;
using Constants;
using Mechanics.Player;
using UnityEngine;
using UserInput;

namespace Core.States
{
    public class GameplayState : State
    {
        private const float VelocityMultiplier = 0.05f;
        private const float MaxPitch = 1.35f;
        
        private readonly IDirectionReader _input;
        private readonly IPlayerMovement _player;
        private readonly IAudioService _audioService;

        public GameplayState(IDirectionReader input, IPlayerMovement player, IAudioService audioService)
        {
            _input = input;
            _player = player;
            _audioService = audioService;
        }

        public override void Enter()
        {
            _player.ControlEnable();
        }

        public override void Update()
        {
            _player.SetDirection(_input.Direction);
            float pitch = GetPitch();
            _audioService.SetPitch(AudioConstants.MASTER_PITCH, pitch);
        }

        private float GetPitch()
        {
            return Mathf.Min(1f + GetVelocity() * VelocityMultiplier, MaxPitch);
        }

        private float GetVelocity()
        {
            return Mathf.Abs(_player.Velocity.x);
        }

        public override void Exit()
        {
            _player.ControlDisable();
        }
    }
}