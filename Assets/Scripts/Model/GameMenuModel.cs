using System;

namespace Model
{
    public class GameMenuModel : IGameMenuModel
    {
        public event Action<bool> OnMusicMuteChangedEvent;
        public event Action<bool> OnSoundMuteChangedEvent;
        public event Action<bool> OnPauseChangedEvent;

        private bool _isPause;
        private bool _soundMute;
        private bool _musicMute;

        public bool IsExit { get; set; }
        public bool IsPause => _isPause;
        public bool SoundMute => _soundMute;
        public bool MusicMute => _musicMute;

        public void SetMusicMute(bool value)
        {
            _musicMute = value;
            OnMusicMuteChangedEvent?.Invoke(value);
        }
        
        public void SetSoundMute(bool value)
        {
            _soundMute = value;
            OnSoundMuteChangedEvent?.Invoke(value);
        }

        public void SetPause(bool value)
        {
            _isPause = value;
            OnPauseChangedEvent?.Invoke(value);
        }
    }
}