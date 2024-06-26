using Platformer.Mechanics;
using UserInput;

namespace Local.Features.StateMachine
{
    public class GameplayState : State
    {
        private readonly IDirectionReader _input;
        private readonly IDirectionApply _player;

        public GameplayState(IDirectionReader input, IDirectionApply player)
        {
            _input = input;
            _player = player;
        }

        public override void Update()
        {
            _player.ApplyDirection(_input.Direction);
        }
    }
}