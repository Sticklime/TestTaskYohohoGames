using CodeBase.Logic.Camera.Component;
using CodeBase.Logic.Character;
using CodeBase.Logic.Global;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Logic.Camera.System
{
    public class CameraFollowSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TransformComponent, PlayerTag> _playerFilter = null;
        private readonly EcsFilter<TransformComponent, CameraFollowComponent> _cameraFilter = null;

        public void Run()
        {
            ref var playerTransformComponent = ref _playerFilter.Get1(0);

            foreach (var i in _cameraFilter)
            {
                ref var cameraTransformComponent = ref _cameraFilter.Get1(i);
                ref var cameraComponent = ref _cameraFilter.Get2(i);

                ref var cameraTransform = ref cameraTransformComponent.Transform;
                ref var playerTransform = ref playerTransformComponent.Transform;


                Vector3 targetPosition = playerTransform.position + cameraComponent.Offset;

                cameraTransform.position = targetPosition;
            }
        }
    }
}