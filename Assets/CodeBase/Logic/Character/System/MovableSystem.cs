using CodeBase.Logic.Global;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Logic.Character
{
    public class MovableSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag, MovableComponent, DirectionComponent, AnimationComponent> _movableFilter =
            null;

        public void Run()
        {
            foreach (var i in _movableFilter)
            {
                ref var movableComponent = ref _movableFilter.Get2(i);
                ref var directionComponent = ref _movableFilter.Get3(i);
                ref var animatorComponent = ref _movableFilter.Get4(i);

                ref var direction = ref directionComponent.Direction;
                ref var speed = ref movableComponent.Speed;
                ref var characterController = ref movableComponent.CharacterController;

                Vector3 movement = GetMovementVector(direction) * speed * Time.deltaTime;

                if (!characterController.isGrounded)
                    movement.y += Physics.gravity.y * Time.deltaTime;

                characterController.Move(movement);

                animatorComponent.animationState = Mathf.Approximately(characterController.velocity.magnitude, 0)
                    ? AnimationStateType.Idle
                    : AnimationStateType.Run;
            }
        }

        private Vector3 GetMovementVector(Vector3 direction) =>
            (Vector3.right * direction.x + Vector3.forward * direction.y) * -1;
    }
}