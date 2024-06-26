using Platformer.UI.PlayerStats;
using UnityEngine;
using VContainer;

namespace Platformer.Mechanics
{
    public class DistanceHandler : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        
        private IDistanceModel _distanceModel;
        private float _distance;

        [Inject]
        private void Construct(IDistanceModel distanceModel)
        {
            _distanceModel = distanceModel;
        }

        private void Update()
        {
            _distance = Vector2.Distance(transform.position, _spawnPoint.position);
            _distanceModel.SetDistance(Mathf.FloorToInt(_distance));
        }
    }
}