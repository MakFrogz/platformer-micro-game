using Platformer.UI.PlayerStats;
using UnityEngine;
using VContainer;

namespace Platformer.Mechanics
{
    public class HealthHandler : MonoBehaviour, IHealthHandler
    {
        private IHealthModel _healthModel;

        [Inject]
        private void Construct(IHealthModel healthModel)
        {
            _healthModel = healthModel;
        }

        public void Damage()
        {
            _healthModel.DecreaseHealth();
        }

        public void Death()
        {
            _healthModel.SetHealth(0);
        }
    }

    public interface IHealthHandler
    {
        void Damage();
        void Death();
    }
}