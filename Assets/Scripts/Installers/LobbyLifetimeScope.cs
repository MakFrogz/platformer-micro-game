using Constants;
using Model;
using SaveLoad;
using UI.PlayerStats;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installers
{
    public class LobbyLifetimeScope : LifetimeScope
    {
        [SerializeField] private PlayerStatsView _playerStatsView;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_playerStatsView);
            
            builder
                .Register<PlayerModel>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();

            builder
                .Register<PlayerStatsPresenter>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder
                .Register<SaveLoadService>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder.RegisterBuildCallback(Setup);
        }

        private void Setup(IObjectResolver resolver)
        {
            IPlayerModel playerModel = resolver.Resolve<IPlayerModel>();
            ISaveLoadService saveLoadService = resolver.Resolve<ISaveLoadService>();
            
            PlayerData playerData = saveLoadService.Load<PlayerData>(KeyConstants.PLAYER_DATA, new PlayerData(1,0,0));
            
            playerModel.SetHealth(playerData.Health);
            playerModel.SetTokens(playerData.Tokens);
            playerModel.SetDistance(playerData.Distance);
        }
    }
}