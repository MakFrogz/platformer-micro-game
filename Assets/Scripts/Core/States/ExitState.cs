using Constants;
using Model;
using SaveLoad;
using UnityEngine.SceneManagement;

namespace Core.States
{
    public class ExitState : State
    {
        private readonly IPlayerModel _playerModel;
        private readonly IGameMenuModel _gameMenuModel;
        private ISaveLoadService _saveLoadService;

        public ExitState(IPlayerModel playerModel, IGameMenuModel gameMenuModel, ISaveLoadService saveLoadService)
        {
            _playerModel = playerModel;
            _gameMenuModel = gameMenuModel;
            _saveLoadService = saveLoadService;
        }

        public override void Enter()
        {
            PlayerData playerData = new PlayerData(_playerModel.Health, _playerModel.Tokens, _playerModel.Distance);
            AudioData audioData = new AudioData(_gameMenuModel.SoundMute, _gameMenuModel.MusicMute);
            _saveLoadService.Save(KeyConstants.PLAYER_DATA, playerData);
            _saveLoadService.Save(KeyConstants.AUDIO_DATA, audioData);

            SceneManager.LoadScene("Lobby");
        }
    }
}