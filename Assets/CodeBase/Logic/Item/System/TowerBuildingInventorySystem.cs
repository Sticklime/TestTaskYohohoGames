using System.Collections.Generic;
using CodeBase.Logic.Character;
using CodeBase.Logic.Inventory;
using Leopotam.Ecs;

public class TowerBuildingInventorySystem : IEcsRunSystem
{
    private readonly EcsFilter<PickerComponent, ItemStackComponent, PlayerTag> _pickerFilter = null;

    public void Run()
    {
        foreach (var pickerIndex in _pickerFilter)
        {
            ref var pickupStackComponent = ref _pickerFilter.Get2(pickerIndex);
            ref var inventoryItem = ref pickupStackComponent.InventoryItem;

            ArrangeItems(inventoryItem);
        }
    }

    private void ArrangeItems(Stack<EcsEntity> inventoryItem)
    {
        foreach (var itemEntity in inventoryItem)
        {
            if (itemEntity.Get<PickupableComponent>().IsPickup)
                SendMessageAnimation(itemEntity);
        }
    }

    private void SendMessageAnimation(EcsEntity itemEntity) => 
        itemEntity.Get<ItemAnimationEvent>();
}