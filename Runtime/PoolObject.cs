using UnityEngine;

namespace Verelll.ObjectsPool
{
    public abstract class PoolObject<T> : MonoBehaviour
    {
        protected IPoolObjectsContainer<T> _poolContainer { get; private set; }

        internal void SetContainer(IPoolObjectsContainer<T> container) => _poolContainer = container;
		
        public virtual void OnLock() { }
        public virtual void OnFree() { }
    }
}
