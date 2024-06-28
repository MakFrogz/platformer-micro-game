using Model;
using Platformer.Mechanics;

namespace Core.States
{
    public class ResetState : State
    {
        private readonly PlayerModel _playerModel;
        private readonly ITokenController _tokenController;

        public ResetState(PlayerModel playerModel, ITokenController tokenController)
        {
            _playerModel = playerModel;
            _tokenController = tokenController;
        }

        public override void Enter()
        {
            _playerModel.SetHealth(1);
            _playerModel.SetDistance(0);
            _tokenController.Reset();
        }
    }
}