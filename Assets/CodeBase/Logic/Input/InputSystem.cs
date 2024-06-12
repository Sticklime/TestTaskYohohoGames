using CodeBase.Logic.Global;
using CodeBase.Logic.Joystick;
using Leopotam.Ecs;

namespace CodeBase.Logic.Input
{
    public class InputSystem : IEcsRunSystem
    {
        private readonly EcsFilter<DirectionComponent> directionFilter = null;
        private readonly EcsFilter<JoystickComponent> joystickFilter = null;

        public void Run()
        {
            foreach (var i in directionFilter)
            {
                ref var joystick = ref joystickFilter.Get1(i);
                ref var directionComponent = ref directionFilter.Get1(i);

                directionComponent.Direction = joystick.Joystick.Direction;
            }
        }
    }
}