using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.ObjectPools;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public void InitScene(ObjectPool resourcePool, IGameFactory gameFactory)
        {
            for (int i = 0; i < 100; i++)
                resourcePool.LoadPool(gameFactory.CreateResource(Vector3.zero));
        }
    }
}