using Constants;
using Model;
using SaveLoad;
using VContainer;
using VContainer.Unity;

namespace Installers
{
    public class RootLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder
                .Register<PlayerModel>(Lifetime.Singleton)
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
            
            PlayerData playerData = saveLoadService.Load(KeyConstants.PLAYER_DATA, new PlayerData(1,0,0));
            
            playerModel.SetHealth(playerData.Health);
            playerModel.SetTokens(playerData.Tokens);
            playerModel.SetDistance(playerData.Distance);
        }
    }
}