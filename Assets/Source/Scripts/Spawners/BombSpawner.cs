using Source.Scripts.Objects;
using UnityEngine;

namespace Source.Scripts.Spawners
{
    public class BombSpawner : MonoBehaviour
    {
        private Pool<Cube> _cubesPool;
        private Pool<Bomb> _pool;

        private void OnEnable()
        {
            _cubesPool.Released += PerformSpawn;
        }

        private void OnDisable()
        {
            _cubesPool.Released -= PerformSpawn;
        }

        public void Initialize(Pool<Bomb> bombPool, Pool<Cube> cubesPool)
        {
            _pool = bombPool;
            _cubesPool = cubesPool;
        }

        private void PerformSpawn(Vector3 position)
        {
            Bomb bomb = _pool.Take();
            
            bomb.SetPosition(position);
            bomb.InvokeActiveLife();
        }
    }
}

