using Model;
using Platformer.Mechanics;
using UnityEngine;
using VContainer;

namespace Mechanics.Health
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
}