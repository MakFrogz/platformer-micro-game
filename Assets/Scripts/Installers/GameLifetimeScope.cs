using Audio;
using Constants;
using Mechanics.Player;
using Model;
using Platformer.Mechanics;
using SaveLoad;
using UI.GameMenu;
using UI.PlayerStats;
using UnityEngine;
using UnityEngine.Audio;
using UserInput;
using VContainer;
using VContainer.Unity;

namespace Installers
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private PlayerStatsView _playerStatsView;
        [SerializeField] private GameMenuView _gameMenuView;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private PlayerController _player;
        [SerializeField] private TokenController _tokenController;
        [SerializeField] private AudioMixer _audioMixer;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_playerStatsView);
            builder.RegisterComponent(_gameMenuView);
            
            builder
                .RegisterComponent(_joystick)
                .AsImplementedInterfaces();

            builder
                .RegisterComponent(_player)
                .AsImplementedInterfaces();

            builder
                .RegisterComponent(_tokenController)
                .AsImplementedInterfaces();

            builder
                .Register<PlayerModel>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();

            builder
                .Register<PlayerStatsPresenter>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();

            builder
                .Register<GameMenuModel>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();

            builder
                .Register<GameMenuPresenter>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();

            builder
                .Register<AudioService>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf()
                .WithParameter(_audioMixer);

            builder
                .Register<SaveLoadService>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}