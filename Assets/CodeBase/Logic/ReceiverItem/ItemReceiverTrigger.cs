using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Logic.ReceiverItem
{
    public class ItemReceiverTrigger : MonoBehaviour
    {
        [SerializeField] private string targetTag = "Player";
        [SerializeField] private EntityReference _entityReference;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(targetTag))
                return;

            if (!_entityReference.Entity.Has<ItemReceiverEvent>()) 
                _entityReference.Entity.Get<ItemReceiverEvent>();
        }
    }
}