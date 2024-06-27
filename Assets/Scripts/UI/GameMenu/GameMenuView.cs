using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GameMenu
{
    public class GameMenuView : MonoBehaviour
    {
        public event Action<bool> OnSoundToggledEvent;
        public event Action<bool> OnMusicToggledEvent;
        public event Action OnExitEvent;
        public event Action OnCloseEvent;
        
        [SerializeField] private Toggle _soundToggle;
        [SerializeField] private Toggle _musicToggle;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _closeButton;

        private void Start()
        {
            _soundToggle.onValueChanged.AddListener(OnSoundToggled);
            _musicToggle.onValueChanged.AddListener(OnMusicToggled);
            _exitButton.onClick.AddListener(OnExit);
            _closeButton.onClick.AddListener(OnClose);
        }

        public void SetMusicToggle(bool value)
        {
            _musicToggle.SetIsOnWithoutNotify(value);
        }

        public void SetSoundToggle(bool value)
        {
            _soundToggle.SetIsOnWithoutNotify(value);
        }

        private void OnSoundToggled(bool value)
        {
            OnSoundToggledEvent?.Invoke(value);
        }
        
        private void OnMusicToggled(bool value)
        {
            OnMusicToggledEvent?.Invoke(value);
        }

        private void OnExit()
        {
            OnExitEvent?.Invoke();
        }

        private void OnClose()
        {
            OnCloseEvent?.Invoke();
        }

        private void OnDestroy()
        {
            _soundToggle.onValueChanged.RemoveListener(OnSoundToggled);
            _musicToggle.onValueChanged.RemoveListener(OnMusicToggled);
            _exitButton.onClick.RemoveListener(OnExit);
            _closeButton.onClick.RemoveListener(OnClose);
        }
    }
}