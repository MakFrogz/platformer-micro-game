using Cinemachine;
using Platformer.Mechanics;
using UnityEngine;

namespace Local.Features.StateMachine
{
    public class TeleportState : State
    {
        private readonly CinemachineVirtualCamera _virtualCamera;
        private readonly PlayerController _player;
        private readonly Transform _spawnPoint;
        
        private readonly int _dead = Animator.StringToHash("dead");

        public TeleportState(CinemachineVirtualCamera virtualCamera, PlayerController player, Transform spawnPoint)
        {
            _virtualCamera = virtualCamera;
            _player = player;
            _spawnPoint = spawnPoint;
        }

        public override void Enter()
        {
            _player.collider2d.enabled = true;
            if (_player.audioSource && _player.respawnAudio)
                _player.audioSource.PlayOneShot(_player.respawnAudio);
            _player.Teleport(_spawnPoint.position);
            _player.jumpState = PlayerController.JumpState.Grounded;
            _player.Animator.SetBool(_dead, false);
            _virtualCamera.m_Follow = _player.transform;
            _virtualCamera.m_LookAt = _player.transform;
        }
    }
}