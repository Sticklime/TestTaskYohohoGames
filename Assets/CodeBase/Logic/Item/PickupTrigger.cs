using CodeBase.Logic.Inventory;
using Leopotam.Ecs;
using UnityEngine;

public class PickupTrigger : MonoBehaviour
{
    [SerializeField] private string _targetTag = "Player";
    [SerializeField] private EntityReference _entityReference;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(_targetTag))
            return;

        if (!_entityReference.Entity.Has<PickupableEvent>())
        {
            _entityReference.Entity.Get<PickupableEvent>();
        }
    }
}