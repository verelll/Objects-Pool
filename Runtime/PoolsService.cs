using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Verelll.ObjectsPool
{
	public static class PoolsService 
	{
		private const string PoolsRootName = "[Pools]";
		private static readonly Dictionary<Type, IPoolObjectsContainer> _containers = new Dictionary<Type, IPoolObjectsContainer>();
		private static readonly Transform _root;
		
		static PoolsService()
		{
			_root = new GameObject(PoolsRootName).transform;
			_root.gameObject.SetActive(false);
			Object.DontDestroyOnLoad(_root);
		}

		public static void DisposePool<T>(IPoolObjectsContainer<T> pool) where T : PoolObject<T>, new()
		{
			pool.Dispose();
		}
		
		public static IPoolObjectsContainer<T> GetOrCreatePool<T>(T prefab, int defaultCount = 0) where T : PoolObject<T>, new()
		{
			var type = prefab.GetType();
			if (_containers.TryGetValue(type, out var pool))
				return (PoolObjectsContainer<T>) pool;

			return CreatePool(prefab, defaultCount);
		}

		private static IPoolObjectsContainer<T> CreatePool<T>(T prefab, int defaultCount) where T : PoolObject<T>, new()
		{
			var newContainer = new PoolObjectsContainer<T>(prefab.name, _root, prefab);
			var pool = (IPoolObjectsContainer) newContainer;
			pool.Init(defaultCount);
			_containers[prefab.GetType()] = newContainer;
			return newContainer;
		}
	}
}
