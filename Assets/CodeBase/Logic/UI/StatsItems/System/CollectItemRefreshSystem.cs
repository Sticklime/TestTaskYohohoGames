using Leopotam.Ecs;

namespace CodeBase.Logic.UI.StatsItems.System
{
    public class CollectItemRefreshSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CollectItemTextComponent> _collectFilter;
        private readonly EcsFilter<ItemReceiverComponent, RefreshTextEvent> _itemReciver;

        public void Run()
        {
            foreach (var i in _itemReciver)
            {
                ref var collectItemTextComponent = ref _collectFilter.Get1(i);
                ref var itemReceiverComponent = ref _itemReciver.Get1(i);
               
                collectItemTextComponent.Text.text = itemReceiverComponent.CountCollectItem.ToString();
            }
        }
    }
}