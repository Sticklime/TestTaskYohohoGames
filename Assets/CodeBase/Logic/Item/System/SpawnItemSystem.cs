using CodeBase.Infrastructure.ObjectPools;
using CodeBase.Logic.Cooldown;
using CodeBase.Logic.Global;
using CodeBase.Logic.Item.Component;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Logic.Item.System
{
    public class SpawnItemSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ItemSpawnerTag, TransformComponent, CooldownEvent> _intemSpanwnerFilter = null;
        private readonly ObjectPool _objectPool = null;

        public void Run()
        {
            foreach (var i in _intemSpanwnerFilter)
            {
                ref var transformComponent = ref _intemSpanwnerFilter.Get2(i);
                ref var transform = ref transformComponent.Transform;

                var resource = _objectPool.GetPool();
                
                if (resource == null)
                    return;

                resource.transform.position = transform.position;
            }
        }
    }
}