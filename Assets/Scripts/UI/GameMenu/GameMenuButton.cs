using Model;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UI.GameMenu
{
    [RequireComponent(typeof(Button))]
    public class GameMenuButton : MonoBehaviour
    {
        private Button _button;
        private IGameMenuModel _model;

        [Inject]
        private void Construct(IGameMenuModel model)
        {
            _model = model;
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(Pause);
        }

        private void Pause()
        {
            _model.SetPause(true);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(Pause);
        }
    }
}