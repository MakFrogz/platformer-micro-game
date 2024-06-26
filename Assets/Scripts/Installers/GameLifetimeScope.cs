using Platformer.Mechanics;
using Platformer.UI.PlayerStats;
using UnityEngine;
using UserInput;
using VContainer;
using VContainer.Unity;

namespace Installers
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private PlayerStatsView _playerStatsView;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private PlayerController _player;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_playerStatsView);
            
            builder
                .RegisterComponent(_joystick)
                .AsImplementedInterfaces();

            builder
                .RegisterComponent(_player)
                .AsImplementedInterfaces();

            builder
                .Register<PlayerModel>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();

            builder
                .Register<PlayerStatsPresenter>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();

            builder.RegisterBuildCallback(Setup);
        }

        private void Setup(IObjectResolver resolver)
        {
            PlayerModel playerModel = resolver.Resolve<PlayerModel>();
            playerModel.SetHealth(1);
            playerModel.SetTokens(0);
            playerModel.SetDistance(0);
        }
    }
}