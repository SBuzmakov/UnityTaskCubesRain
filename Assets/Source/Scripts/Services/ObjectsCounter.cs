using System;
using Source.Scripts.Objects;
using Source.Scripts.Spawners;
using UnityEngine;

namespace Source.Scripts.Services
{
    public class ObjectsCounter<T> : MonoBehaviour where T : MonoBehaviour, ISpawnable<T>
    {
        [SerializeField] Pool<T> _pool;
        
        public event Action<int> ChangedCreatedCount;
        public event Action<int> ChangedActiveCount;
        public event Action<int> ChangedSpawnedCount;
        
        public int CreatedCount { get; private set; }
        public int ActiveCount { get; private set; }
        public int SpawnedCount { get; private set; }

        private void OnEnable()
        {
            _pool.Created += IncreaseCreatedCount;
            _pool.Activated += IncreaseActiveCount;
            _pool.Deactivated += DecreaseActiveCount;
            _pool.Activated += IncreaseSpawnedCount;
        }
        
        private void OnDisable()
        {
            _pool.Created -= IncreaseCreatedCount;
            _pool.Activated -= IncreaseActiveCount;
            _pool.Deactivated -= DecreaseActiveCount;
            _pool.Activated -= IncreaseSpawnedCount;
        }
        
        private void Awake()
        {
            CreatedCount = 0;
            ActiveCount = 0;
            SpawnedCount = 0;
        }

        private void IncreaseCreatedCount()
        {
            CreatedCount++;
            ChangedCreatedCount?.Invoke(CreatedCount);
        }

        private void IncreaseActiveCount()
        {
            ActiveCount++;
            ChangedActiveCount?.Invoke(ActiveCount);
        }

        private void DecreaseActiveCount()
        {
            ActiveCount--;
            ChangedActiveCount?.Invoke(ActiveCount);
        }

        private void IncreaseSpawnedCount()
        {
            SpawnedCount++;
            ChangedSpawnedCount?.Invoke(SpawnedCount);
        }
    }
}
