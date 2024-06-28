using Mechanics.Player;
using Platformer.Mechanics;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Boosts
{
    [RequireComponent(typeof(Button))]
    public class BoostButton : MonoBehaviour
    {
        [SerializeField] private BoostData _boostData;

        private IBoostHandler _boostHandler;
        private Button _button;

        [Inject]
        private void Construct(IBoostHandler boostHandler)
        {
            _boostHandler = boostHandler;
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(OnActivate);
        }

        private void OnActivate()
        {
            _boostHandler.ApplyBoost(_boostData);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnActivate);
        }
    }
}