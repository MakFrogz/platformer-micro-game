using Model;

namespace Core.States
{
    public class ResetState : State
    {
        private readonly PlayerModel _playerModel;

        public ResetState(PlayerModel playerModel)
        {
            _playerModel = playerModel;
        }

        public override void Enter()
        {
            _playerModel.SetHealth(1);
            _playerModel.SetDistance(0);
        }
    }
}