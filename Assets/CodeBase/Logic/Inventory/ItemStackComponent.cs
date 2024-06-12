using System;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Logic.Inventory
{
    [Serializable]
    public struct ItemStackComponent
    {
        public Stack<EcsEntity> InventoryItem;
        public float DistanceBehindPlayer;
        public float ItemHeightOffset;
        public Vector3 AngleItemInStack;
    }
}