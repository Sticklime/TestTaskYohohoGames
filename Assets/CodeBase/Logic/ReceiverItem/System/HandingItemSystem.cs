using CodeBase.Infrastructure.ObjectPools;
using CodeBase.Logic.Global;
using CodeBase.Logic.Inventory;
using CodeBase.Logic.UI.StatsItems.System;
using Leopotam.Ecs;
using UnityEngine;

public class HandingItemSystem : IEcsRunSystem
{
    private readonly EcsFilter<ItemReceiverEvent, TransformComponent, ItemReceiverComponent> _receiverFilter = null;
    private readonly EcsFilter<ItemStackComponent> _pickerFilter = null;
    private readonly EcsFilter<PickupableComponent, PhysicComponent> _itemFilter = null;

    private ObjectPool _itemPool;

    public void Run()
    {
        foreach (var i in _receiverFilter)
        {
            ref var targetTransform = ref _receiverFilter.Get2(i).Transform;
            ref var receiverComponent = ref _receiverFilter.Get3(i);
            ref var receiverEntity = ref _receiverFilter.GetEntity(i);
            ref var pickerEntity = ref _pickerFilter.GetEntity(i);
            ref var itemStackComponent = ref _pickerFilter.Get1(i);
            ref var itemStack = ref itemStackComponent.InventoryItem;

            foreach (EcsEntity itemEntity in itemStack.ToArray())
            {
                ref var itemTransform = ref itemEntity.Get<TransformComponent>().Transform;

                itemTransform.position = MoveToTarget(itemTransform, targetTransform, receiverComponent.MoveSpeed);

                if (Vector3.Distance(itemTransform.position, targetTransform.position) < 0.1f)
                {
                    receiverComponent.CountCollectItem++;
                    _itemPool.ReturnToPool(itemEntity.Get<TransformComponent>().Transform.gameObject);
                    itemStack.Pop();
                }
            }

            if (itemStack.Count == 0)
            {
                ResetItem();
                pickerEntity.Get<RefreshTextEvent>();
                receiverEntity.Get<RefreshTextEvent>();
                receiverEntity.Del<ItemReceiverEvent>();
            }
        }
    }

    private void ResetItem()
    {
        foreach (var i in _itemFilter)
        {
            ref var pickupable = ref _itemFilter.Get1(i);
            ref var itemPhysic = ref _itemFilter.Get2(i);

            itemPhysic.Rigibody.isKinematic = false;
            itemPhysic.Collider.enabled = true;
            pickupable.IsPickup = false;
        }
    }

    private Vector3 MoveToTarget(Transform itemTransform, Transform targetTransform, float moveSpeed) =>
        Vector3.MoveTowards(itemTransform.position, targetTransform.position, Time.deltaTime * moveSpeed);
}