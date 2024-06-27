using System;

namespace Model
{
    public interface IGameMenuModel
    {
        event Action<bool> OnMusicMuteChangedEvent;
        event Action<bool> OnSoundMuteChangedEvent;
        event Action<bool> OnPauseChangedEvent;

        bool IsExit { get; set; }
        bool IsPause { get; }
        bool MusicMute { get; }
        bool SoundMute { get; }
        void SetMusicMute(bool value);
        void SetSoundMute(bool value);
        void SetPause(bool value);
    }
}