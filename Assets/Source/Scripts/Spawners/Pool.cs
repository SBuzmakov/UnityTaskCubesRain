using System;
using Source.Scripts.Objects;
using UnityEngine;
using UnityEngine.Pool;

namespace Source.Scripts.Spawners
{
    public class Pool<T>:MonoBehaviour where T : MonoBehaviour , ISpawnable<T>
    {
        private ObjectFactory<T> _objectFactory;
        
        private ObjectPool<T> _pool;

        public event Action Activated;
        public event Action Deactivated;
        public event Action Created;
        public event Action<Vector3> Released;

        public void Initialize(ObjectFactory<T> objectFactory)
        {
            _objectFactory = objectFactory;
            
            _pool = new ObjectPool<T>(
                createFunc: InvokeCreate,
                actionOnRelease: OnReturnToPool,
                actionOnDestroy: OnDestroyObject,
                collectionCheck: false
            );
        }

        public T Take()
        {
            T obj = _pool.Get();
            obj.gameObject.SetActive(true);
            Activated?.Invoke();
            return obj;
        }

        private void Release(T obj)
        {
            _pool.Release(obj);
            Deactivated?.Invoke();
            Released?.Invoke(obj.transform.position);
        }

        private T InvokeCreate()
        {
            T obj = _objectFactory.Create();
            Created?.Invoke();
            obj.ActiveLifeFinished += Release;
            return obj;
        }
        
        private void OnReturnToPool(T obj)
        {
            obj.gameObject.SetActive(false);
        }
        
        private void OnDestroyObject(T obj)
        {
            obj.ActiveLifeFinished -= Release;
            
            Destroy(obj.gameObject);
        }
    }
}