using System;
using Local.Features.StateMachine;
using Platformer.Mechanics;
using Platformer.UI.PlayerStats;
using UnityEngine;
using UserInput;
using Utils;
using VContainer;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Cinemachine.CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField][Min(0)] private float _respawnTime;
        
        private StateMachine _stateMachine;

        private IDirectionReader _joystick;
        private PlayerController _playerController;
        private PlayerModel _playerModel;

        private CountdownTimer _respawnTimer;
        
        [Inject]
        private void Construct(IDirectionReader joystick, PlayerController playerController, PlayerModel playerModel)
        {
            _joystick = joystick;
            _playerController = playerController;
            _playerModel = playerModel;
        }
        
        private void Awake()
        {
            _stateMachine = new StateMachine();
        }

        private void Start()
        {
            InitializeTimers();
            InitializeStateMachine();
        }

        private void InitializeTimers()
        {
            _respawnTimer = new CountdownTimer(_respawnTime);
        }

        private void InitializeStateMachine()
        {
            IState setupState = new SetupState(_playerModel);
            IState gameplayState = new GameplayState(_joystick, _playerController);
            IState deadState = new DeadState(_virtualCamera, _playerController, _respawnTimer);
            IState teleportState = new TeleportState(_virtualCamera, _playerController, _spawnPoint);
            IState achievementState = new AchievementState();

            _stateMachine.AddTransaction(setupState, gameplayState, new FuncPredicate(() => true));
            _stateMachine.AddTransaction(gameplayState, deadState, new FuncPredicate(() => !_playerModel.IsAlive));
            _stateMachine.AddTransaction(deadState, teleportState, new FuncPredicate(() => _respawnTimer.IsFinished));
            _stateMachine.AddTransaction(teleportState, setupState, new FuncPredicate(() => true));

            _stateMachine.SetState(setupState);
        }

        private void Update()
        {
            _stateMachine.Update();
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            _respawnTimer.Tick(Time.deltaTime);
        }
    }
}