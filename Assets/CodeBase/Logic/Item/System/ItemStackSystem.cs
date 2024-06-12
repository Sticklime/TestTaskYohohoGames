using CodeBase.Logic.Character;
using CodeBase.Logic.Global;
using CodeBase.Logic.Inventory;
using Leopotam.Ecs;
using UnityEngine;

public class StuckBuildingAnimationSystem : IEcsRunSystem
{
    private readonly EcsFilter<PickerComponent, TransformComponent, ItemStackComponent> _pickerFilter = null;

    public void Run()
    {
        foreach (var pickerIndex in _pickerFilter)
        {
            ref var pickerComponent = ref _pickerFilter.Get1(pickerIndex);
            ref var pickerTransformComponent = ref _pickerFilter.Get2(pickerIndex);
            ref var stackItem = ref _pickerFilter.Get3(pickerIndex);

            ref var pickerTransform = ref pickerTransformComponent.Transform;
            ref var inventoryItem = ref stackItem.InventoryItem;
            ref var distanceBehindPlayer = ref stackItem.DistanceBehindPlayer;
            ref var angleItemInStack = ref stackItem.AngleItemInStack;
            ref var itemHeightOffset = ref stackItem.ItemHeightOffset;

            int index = 0;

            foreach (var itemEntity in inventoryItem)
            {
                var backOffset = GetBackOffset(pickerTransform, distanceBehindPlayer);

                Vector3 targetPosition = 
                    pickerTransform.position + backOffset + Vector3.up * (index * itemHeightOffset);
                Quaternion targetRotation = Quaternion.Euler(angleItemInStack);

                AnimateItem(itemEntity.Get<TransformComponent>().Transform, 
                    targetPosition, targetRotation, pickerComponent);

                if (!CheckProgress(pickerComponent))
                    itemEntity.Del<ItemAnimationEvent>();

                index++;
            }
        }
    }

    private void AnimateItem(Transform transform, Vector3 targetPosition, Quaternion targetRotation,
        PickerComponent pickerComponent)
    {
        if (CheckProgress(pickerComponent))
        {
            SmoothMove(transform, targetPosition, GetProgress(pickerComponent));
            SmoothRotate(transform, targetRotation, GetProgress(pickerComponent));
        }
        else
        {
            SetFinalPosition(transform, targetPosition, targetRotation);
        }
    }

    private bool CheckProgress(PickerComponent pickerComponent) =>
        GetProgress(pickerComponent) < 1.0f;

    private float GetProgress(PickerComponent pickerComponent) =>
        pickerComponent.AnimationDuration > 0
            ? Mathf.Clamp01(Time.deltaTime / pickerComponent.AnimationDuration)
            : 1f;

    private Vector3 GetBackOffset(Transform pickerTransform, float distanceBehindPlayer)
    {
        Vector3 directionToPlayer = pickerTransform.position - pickerTransform.forward * distanceBehindPlayer;
        Vector3 backOffset = -directionToPlayer.normalized;
        return backOffset;
    }

    private void SmoothMove(Transform transform, Vector3 targetPosition, float progress) =>
        transform.position = Vector3.Lerp(transform.position, targetPosition, progress);

    private void SmoothRotate(Transform transform, Quaternion targetRotation, float progress) =>
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, progress);

    private void SetFinalPosition(Transform transform, Vector3 targetPosition, Quaternion targetRotation)
    {
        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }
}