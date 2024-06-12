using System.Collections.Generic;
using CodeBase.Logic.Character;
using CodeBase.Logic.Global;
using CodeBase.Logic.UI.StatsItems.System;
using Leopotam.Ecs;

namespace CodeBase.Logic.Inventory
{
    public class PickupSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PickupableEvent, TransformComponent, PickupableComponent, PhysicComponent> _pickupableFilter = null;
        private readonly EcsFilter<PickerComponent, ItemStackComponent, TransformComponent, PlayerTag> _pickerFilter = null;

        public void Run()
        {
            foreach (var pickerIndex in _pickerFilter)
            {
                ref var pickerEntity = ref _pickerFilter.GetEntity(pickerIndex);
                ref var pickupStackComponent = ref _pickerFilter.Get2(pickerIndex);

                ref var inventoryItem = ref pickupStackComponent.InventoryItem;
                inventoryItem ??= new Stack<EcsEntity>();

                foreach (var pickupableIndex in _pickupableFilter)
                {
                    ref var pickupableEntity = ref _pickupableFilter.GetEntity(pickupableIndex);
                    ref var pickupablePhysicComponent = ref _pickupableFilter.Get4(pickupableIndex);
                    ref var pickupableComponent = ref _pickupableFilter.Get3(pickupableIndex);

                    if (pickupableComponent.IsPickup)
                    {
                        pickupableEntity.Del<PickupableEvent>();
                    }
                    else
                    {
                        pickupableComponent.IsPickup = true;
                        inventoryItem.Push(pickupableEntity);
                        pickupablePhysicComponent.Rigibody.isKinematic = true;
                        pickerEntity.Get<RefreshTextEvent>();
                        pickupableEntity.Del<PickupableEvent>();
                    }
                }
            }
        }
    }
}