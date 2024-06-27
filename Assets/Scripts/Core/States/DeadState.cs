using Cinemachine;
using Mechanics.Player;
using UnityEngine;
using Utils;

namespace Core.States
{
    public class DeadState : State
    {
        private readonly CinemachineVirtualCamera _virtualCamera;
        private readonly PlayerController _player;
        private readonly CountdownTimer _countdownTimer;
        
        private readonly int _hurt = Animator.StringToHash("hurt");
        private readonly int _dead = Animator.StringToHash("dead");

        public DeadState(CinemachineVirtualCamera virtualCamera, PlayerController player, CountdownTimer countdownTimer)
        {
            _virtualCamera = virtualCamera;
            _player = player;
            _countdownTimer = countdownTimer;
        }

        public override void Enter()
        {
            _virtualCamera.m_Follow = null;
            _virtualCamera.m_LookAt = null;

            if (_player.audioSource && _player.ouchAudio)
            {
                _player.audioSource.PlayOneShot(_player.ouchAudio);
            }
            _player.Animator.SetTrigger(_hurt);
            _player.Animator.SetBool(_dead, true);
            
            _countdownTimer.Start();
        }
    }
}