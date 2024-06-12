using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.ObjectPools
{
    public class ObjectPool 
    {
        private readonly Dictionary<string, GameObject> _poolObject = new();

        public void LoadPool(params GameObject[] objects)
        {
            foreach (GameObject gameObject in objects)
            {
                gameObject.SetActive(false);
                _poolObject.Add(GetUniqueIdentifier(gameObject), gameObject);
            }
        }

        public GameObject GetPool()
        {
            foreach (var gameObject in _poolObject)
            {
                if (!gameObject.Value.activeSelf)
                {
                    gameObject.Value.SetActive(true);

                    return gameObject.Value;
                }
            }

            return null;
        }

        public void ReturnToPool(GameObject gameObject)
        {
            if (_poolObject.ContainsKey(gameObject.GetInstanceID().ToString()))
                gameObject.SetActive(false);
        }

        private string GetUniqueIdentifier(GameObject gameObject) =>
            gameObject.GetInstanceID().ToString();
    }
}