using Platformer.UI.PlayerStats;

namespace Local.Features.StateMachine
{
    public class SetupState : State
    {
        private PlayerModel _playerModel;

        public SetupState(PlayerModel playerModel)
        {
            _playerModel = playerModel;
        }

        public override void Enter()
        {
            _playerModel.SetHealth(1);
            _playerModel.SetTokens(0);
            _playerModel.SetDistance(0);
        }
    }
}