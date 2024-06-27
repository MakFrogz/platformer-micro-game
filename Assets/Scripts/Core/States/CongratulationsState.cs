using Platformer.Mechanics;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils;

namespace Core.States
{
    public class CongratulationsState : State
    {
        private readonly CountdownTimer _congratulationsTimer;
        private readonly AssetReference _congratulationsPrefab;
        private readonly ITokenController _tokenController;

        private GameObject _view;

        public CongratulationsState(CountdownTimer congratulationsTimer, AssetReference congratulationsPrefab, ITokenController tokenController)
        {
            _congratulationsTimer = congratulationsTimer;
            _congratulationsPrefab = congratulationsPrefab;
            _tokenController = tokenController;
        }

        public override void Enter()
        {
            var prefab = Addressables.LoadAssetAsync<GameObject>(_congratulationsPrefab).WaitForCompletion();
            _view = Object.Instantiate(prefab);
            _congratulationsTimer.Start();
        }

        public override void Exit()
        {
            _tokenController.Reset();
            Object.Destroy(_view.gameObject);
        }
    }
}