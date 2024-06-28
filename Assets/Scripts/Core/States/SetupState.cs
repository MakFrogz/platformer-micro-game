using Audio;
using Constants;
using Model;
using SaveLoad;

namespace Core.States
{
    public class SetupState : State
    {
        private IPlayerModel _playerModel;
        private IGameMenuModel _gameMenuModel;
        private ISaveLoadService _saveLoadService;
        private IAudioService _audioService;

        public SetupState(IPlayerModel playerModel, IGameMenuModel gameMenuModel, ISaveLoadService saveLoadService, IAudioService audioService)
        {
            _playerModel = playerModel;
            _gameMenuModel = gameMenuModel;
            _saveLoadService = saveLoadService;
            _audioService = audioService;
        }

        public override void Enter()
        {
            PlayerData playerData = _saveLoadService.Load<PlayerData>(KeyConstants.PLAYER_DATA, new PlayerData(1,0,0));
            AudioData audioData = _saveLoadService.Load<AudioData>(KeyConstants.AUDIO_DATA, new AudioData(false, false));
            
            _playerModel.SetHealth(playerData.Health);
            _playerModel.SetTokens(playerData.Tokens);
            _playerModel.SetDistance(playerData.Distance);
            
            _gameMenuModel.SetSoundMute(audioData.SoundMute);
            _gameMenuModel.SetMusicMute(audioData.MusicMute);
            
            _audioService.Mute(AudioConstants.SOUND_VOLUME, audioData.SoundMute);
            _audioService.Mute(AudioConstants.MUSIC_VOLUME, audioData.MusicMute);
        }
    }
}