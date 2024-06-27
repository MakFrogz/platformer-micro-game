using Audio;
using Constants;
using Model;
using UnityEngine;

namespace Core.States
{
    public class PauseState : State
    {
        private readonly IGameMenuModel _gameMenuModel;
        private readonly IAudioService _audioService;

        public PauseState(IGameMenuModel gameMenuModel, IAudioService audioService)
        {
            _gameMenuModel = gameMenuModel;
            _audioService = audioService;
        }

        public override void Enter()
        {
            _gameMenuModel.OnMusicMuteChangedEvent += MusicMute;
            _gameMenuModel.OnSoundMuteChangedEvent += SoundMute;

            Time.timeScale = 0f;
        }

        private void SoundMute(bool value)
        {
            _audioService.Mute(AudioConstants.SOUND_VOLUME, value);
        }

        private void MusicMute(bool value)
        {
            _audioService.Mute(AudioConstants.MUSIC_VOLUME, value);
        }

        public override void Exit()
        {
            _gameMenuModel.OnMusicMuteChangedEvent -= MusicMute;
            _gameMenuModel.OnSoundMuteChangedEvent -= SoundMute;

            Time.timeScale = 1f;
        }
    }
}