using CodeBase.Infrastructure.ObjectPools;
using CodeBase.Logic.Camera.System;
using CodeBase.Logic.Character;
using CodeBase.Logic.Cooldown;
using CodeBase.Logic.Global;
using CodeBase.Logic.Input;
using CodeBase.Logic.Inventory;
using CodeBase.Logic.Item.System;
using CodeBase.Logic.UI.StatsItems.System;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace CodeBase.Infrastructure.EntryPoint
{
    public class EcsBootstrapper : MonoBehaviour
    {
        private ObjectPool _resourcesPool;
        private EcsSystems _systems;
        private EcsWorld _world;

        public void Construct(ObjectPool resourcesPool)
        {
            _resourcesPool = resourcesPool;
        }

        public void StartSystem()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            AddSystem();
            AddOneFrameComponent();

            _systems.ConvertScene();

            _systems.Init();
        }

        private void Update()
        {
            if (_systems == null)
                return;

            _systems.Run();
        }


        private void AddOneFrameComponent()
        {
            _systems
                .OneFrame<CooldownEvent>()
                .OneFrame<RefreshTextEvent>();
        }

        private void AddSystem()
        {
            _systems
                .Add(new EntityInitializeSystem())
                .Add(new CooldownSystem())
                .Add(new InputSystem())
                .Add(new RotationSystem())
                .Add(new MovableSystem())
                .Add(new AnimationSystem())
                .Add(new SpawnItemSystem()).Inject(_resourcesPool)
                .Add(new HandingItemSystem()).Inject(_resourcesPool)
                .Add(new PickupSystem())
                .Add(new TowerBuildingInventorySystem())
                .Add(new StuckBuildingAnimationSystem())
                .Add(new CollectItemRefreshSystem())
                .Add(new ItemInventoryRefreshSystem())
                .Add(new CameraFollowSystem());
        }

        private void OnDestroy()
        {
            _systems.Destroy();
            _systems = null;
            _world.Destroy();
            _world = null;
        }
    }
}