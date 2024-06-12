using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.ObjectPools;
using UnityEngine;

namespace CodeBase.Infrastructure.EntryPoint
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private EcsBootstrapper _ecsBootstrapper;

        private void Awake()
        {
            Game game = new Game();
            ObjectPool resourcePool = new ObjectPool();
            IGameFactory gameFactory = new GameFactory();

            gameFactory.Load();

            _ecsBootstrapper.Construct(resourcePool);
            _ecsBootstrapper.StartSystem();

            game.InitScene(resourcePool, gameFactory);
        }
    }
}