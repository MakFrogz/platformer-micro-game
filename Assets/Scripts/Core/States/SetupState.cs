using Audio;
using Constants;
using Model;
using SaveLoad;

namespace Core.States
{
    public class SetupState : State
    {
        private IGameMenuModel _gameMenuModel;
        private ISaveLoadService _saveLoadService;
        private IAudioService _audioService;

        public SetupState(IGameMenuModel gameMenuModel, ISaveLoadService saveLoadService, IAudioService audioService)
        {
            _gameMenuModel = gameMenuModel;
            _saveLoadService = saveLoadService;
            _audioService = audioService;
        }

        public override void Enter()
        {
            AudioData audioData = _saveLoadService.Load<AudioData>(KeyConstants.AUDIO_DATA, new AudioData(false, false));
            
            _gameMenuModel.SetSoundMute(audioData.SoundMute);
            _gameMenuModel.SetMusicMute(audioData.MusicMute);
            
            _audioService.Mute(AudioConstants.SOUND_VOLUME, audioData.SoundMute);
            _audioService.Mute(AudioConstants.MUSIC_VOLUME, audioData.MusicMute);
        }
    }
}