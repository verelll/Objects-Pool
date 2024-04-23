using UnityEngine;

namespace Verelll.ObjectsPool
{
    public sealed class Example : MonoBehaviour
    {
        private IPoolObjectsContainer<ExampleObject> _examplePool;
        
        private ExampleObject _examplePrefab;
        private Transform _parentTrans;
        
        private void Start()
        {
            var defaultObjectsCount = 10;
            _examplePool = PoolsService.GetOrCreatePool(_examplePrefab, defaultObjectsCount);
            
            
            //Get object from pool
            var newObject = _examplePool.Lock(_parentTrans);
            
            
            //Return the object to the object pool
            _examplePool.Free(newObject);
        }
    }

    public sealed class ExampleObject : PoolObject<ExampleObject> { }
}
