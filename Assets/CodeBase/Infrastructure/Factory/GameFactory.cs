using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private GameObject _resource;

        public void Load()
        {
            _resource = Resources.Load<GameObject>("Prefabs/Item/Resources");
        }

        public GameObject CreateResource(Vector3 at)
        {
            return Object.Instantiate(_resource, at, Quaternion.identity);
        }
    }
}