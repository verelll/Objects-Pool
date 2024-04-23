using UnityEngine;

namespace Verelll.ObjectsPool
{
    public interface IPoolObjectsContainer
    {
        internal void Init(int count);
        internal void Dispose();
    }
    
    public interface IPoolObjectsContainer<T> : IPoolObjectsContainer
    {
        T Lock(Transform parent = null);
        void Free(T view);
    }
}
