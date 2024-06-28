using System;
using Model;
using VContainer.Unity;

namespace UI.PlayerStats
{
    public class PlayerStatsPresenter : IStartable, IDisposable
    {
        private readonly PlayerStatsView _view;
        private readonly IPlayerModel _model;

        public PlayerStatsPresenter(PlayerStatsView view, IPlayerModel model)
        {
            _view = view;
            _model = model;
        }

        public void Start()
        {
            HealthChanged(_model.Health);
            TokensChanged(_model.Tokens);
            DistanceChanged(_model.Distance);
            
            _model.OnHealthChangedEvent += HealthChanged;
            _model.OnTokensChangedEvent += TokensChanged;
            _model.OnDistanceChangedEvent += DistanceChanged;
        }

        private void HealthChanged(int health)
        {
            _view.UpdateHealth(health);
        }
        
        private void TokensChanged(int coins)
        {
            _view.UpdateTokens(coins);
        }
        
        private void DistanceChanged(int distance)
        {
            _view.UpdateDistance(distance);
        }

        public void Dispose()
        {
            _model.OnHealthChangedEvent -= HealthChanged;
            _model.OnTokensChangedEvent -= TokensChanged;
            _model.OnDistanceChangedEvent -= DistanceChanged;
        }
    }
}