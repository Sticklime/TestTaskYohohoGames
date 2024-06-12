using CodeBase.Logic.Inventory;
using CodeBase.Logic.UI.StatsItems.Component;
using Leopotam.Ecs;

namespace CodeBase.Logic.UI.StatsItems.System
{
    public class ItemInventoryRefreshSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InventoryItemTextComponent> _collectFilter;
        private readonly EcsFilter<ItemStackComponent, RefreshTextEvent> _itemReciver;

        public void Run()
        {
            foreach (var i in _itemReciver)
            {
                ref var ItemTextComponent = ref _collectFilter.Get1(i);
                ref var itemStackComponent = ref _itemReciver.Get1(i);

                ItemTextComponent.Text.text = itemStackComponent.InventoryItem.Count.ToString();
            }
        }
    }
}