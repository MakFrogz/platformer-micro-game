using Platformer.Mechanics;
using UnityEngine;
using UserInput;
using VContainer;
using VContainer.Unity;

namespace Installers
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private PlayerController _player;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder
                .RegisterComponent(_joystick)
                .AsImplementedInterfaces();

            builder
                .RegisterComponent(_player)
                .AsImplementedInterfaces();
        }
    }
}