using System.Collections.Generic;
using UnityEngine;

namespace Verelll.ObjectsPool
{
    internal sealed class PoolObjectsContainer<T> : IPoolObjectsContainer<T>
        where T : PoolObject<T>
    {
        public T Prefab { get; }
        public Transform Parent { get; }
        
        private Queue<T> _lockedObjects;

        public PoolObjectsContainer(string name, Transform poolsRoot, T prefab)
        {
            _lockedObjects = new Queue<T>();
            Prefab = prefab;

            Parent = new GameObject($"[{name}]").transform;
            Parent.transform.SetParent(poolsRoot);
            Parent.gameObject.SetActive(false);
        }
        
        void IPoolObjectsContainer.Init(int count)
        {
            for (int i = 0; i < count; i++)
                CreateObject();
        }

        void IPoolObjectsContainer.Dispose()
        {
            foreach (var lockedObject in _lockedObjects)
                Free(lockedObject);
        }
        
        public T Lock(Transform parent = null)
        {
            T lockedObject = null;
            if (_lockedObjects.Count > 0)
            {
                lockedObject = _lockedObjects.Dequeue();
            }
            else
            {
                CreateObject();
                lockedObject = _lockedObjects.Dequeue();
            }
            
            if(parent != null)
                lockedObject.transform.SetParent(parent, false);

            lockedObject.OnLock();
            return lockedObject;
        }

        public void Free(T view)
        {
            _lockedObjects.Enqueue(view);
            view.transform.SetParent(Parent);
            view.OnFree();
        }

        private void CreateObject()
        {
            var newObject = Object.Instantiate(Prefab, Parent);
            newObject.SetContainer(this);
            _lockedObjects.Enqueue(newObject);
        }
    }
}
