using CodeBase.Logic.Global;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Logic.Character
{
    public class RotationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag, DirectionComponent, TransformComponent> _movableFilter = null;

        public void Run()
        {
            foreach (var i in _movableFilter)
            {
                ref var directionComponent = ref _movableFilter.Get2(i);
                ref var transformComponent = ref _movableFilter.Get3(i);

                Vector3 direction = directionComponent.Direction;

                if (direction != Vector3.zero)
                {
                    Vector3 flatDirection = new Vector3(direction.x, 0, direction.y).normalized;
                    Quaternion targetRotation = Quaternion.LookRotation(flatDirection);

                    transformComponent.Transform.rotation =
                        Quaternion.Slerp(transformComponent.Transform.rotation, targetRotation, Time.deltaTime * 10f);
                }
            }
        }
    }
}