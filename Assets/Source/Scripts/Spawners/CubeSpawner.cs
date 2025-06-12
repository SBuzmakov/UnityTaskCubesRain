using System.Collections;
using Source.Scripts.Objects;
using UnityEngine;

namespace Source.Scripts.Spawners
{
    public class CubeSpawner : MonoBehaviour
    {
        [SerializeField] private Collider _ground;
        [SerializeField] private float _cloudHeight;
        [SerializeField, Range(4, 6)] private float _cloudDivisor;
        [SerializeField, Range(1, 10)] private int _rainIntensity;
        [SerializeField, Range(0, 1)] private float _rainFrequency;
        [SerializeField] private bool _isWorking = true;
        
        private Pool<Cube> _pool;
        private WaitForSeconds _wait;
        private Coroutine _coroutine;
        private float _positionY;

        private void OnDestroy()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }

        public void Initialize( Pool<Cube> cubesPool)
        {
            _pool = cubesPool;
            
            _positionY = _ground.transform.position.y + _cloudHeight;

            _coroutine = StartCoroutine(Rain());
            _wait = new WaitForSeconds(_rainFrequency);
        }

        private IEnumerator Rain()
        {
            while (_isWorking)
            {
                GetCubes();

                yield return _wait;
            }
        }

        private void GetCubes()
        {
            for (int i = 0; i < _rainIntensity; i++)
            {
                Cube cube = _pool.Take();

                cube.transform.SetPositionAndRotation(GetRandomCubePosition(), GetRandomCubeRotation());
            }
        }

        private Quaternion GetRandomCubeRotation()
        {
            return Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        }

        private Vector3 GetRandomCubePosition()
        {
            Vector3 center = _ground.bounds.center;
            Vector3 size = _ground.bounds.size;
            
            float positionX = GetRandomPositionValue(center, size);
            
            float positionZ = GetRandomPositionValue(center, size);
            
            return new Vector3(positionX, _positionY, positionZ);
        }

        private float GetRandomPositionValue(Vector3 center, Vector3 size)
        {
            return Random.Range(center.x - size.x / _cloudDivisor, center.x + size.x / _cloudDivisor);
        }
    }
}