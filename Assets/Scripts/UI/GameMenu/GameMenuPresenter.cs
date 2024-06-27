using System;
using Model;
using VContainer.Unity;

namespace UI.GameMenu
{
    public class GameMenuPresenter : IStartable, IDisposable
    {
        private GameMenuView _view;
        private IGameMenuModel _model;

        public GameMenuPresenter(GameMenuView view, IGameMenuModel model)
        {
            _view = view;
            _model = model;
        }

        public void Start()
        {
            _view.SetMusicToggle(!_model.MusicMute);
            _view.SetSoundToggle(!_model.SoundMute);
            
            _view.OnSoundToggledEvent += SoundToggled;
            _view.OnMusicToggledEvent += MusicToggled;
            _view.OnExitEvent += Exit;
            _view.OnCloseEvent += Close;

            _model.OnPauseChangedEvent += PauseChanged;
            _model.OnSoundMuteChangedEvent += SoundMuteChanged;
            _model.OnMusicMuteChangedEvent += MusicMuteChanged;
        }

        private void SoundToggled(bool value)
        {
            _model.SetSoundMute(!value);
        }
        
        private void MusicToggled(bool value)
        {
            _model.SetMusicMute(!value);
        }

        private void Exit()
        {
            _model.IsExit = true;
        }

        private void Close()
        {
            _model.SetPause(false);
        }

        private void PauseChanged(bool value)
        {
            _view.gameObject.SetActive(value);
        }

        private void SoundMuteChanged(bool value)
        {
            _view.SetSoundToggle(!value);
        }
        
        private void MusicMuteChanged(bool value)
        {
            _view.SetMusicToggle(!value);
        }

        public void Dispose()
        {
            _view.OnSoundToggledEvent -= SoundToggled;
            _view.OnMusicToggledEvent -= MusicToggled;
            _view.OnExitEvent -= Exit;
            _view.OnCloseEvent -= Close;
            
            _model.OnPauseChangedEvent -= PauseChanged;
            _model.OnSoundMuteChangedEvent -= SoundMuteChanged;
            _model.OnMusicMuteChangedEvent -= MusicMuteChanged;
        }
    }
}