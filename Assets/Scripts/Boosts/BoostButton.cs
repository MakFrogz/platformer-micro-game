using Platformer.Mechanics;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Boosts
{
    [RequireComponent(typeof(Button))]
    public class BoostButton : MonoBehaviour
    {
        [SerializeField] private AbilityType _abilityType;
        [SerializeField][Min(1)] private float _abilityMultiplier;

        private IBoostApply _player;
        private Button _button;

        [Inject]
        private void Construct(IBoostApply player)
        {
            _player = player;
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
            _player.ApplyBoost(_abilityType, _abilityMultiplier);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnActivate);
        }
    }
}