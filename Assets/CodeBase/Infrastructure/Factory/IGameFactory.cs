using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory
    {
        void Load();
        GameObject CreateResource(Vector3 at);
    }
}