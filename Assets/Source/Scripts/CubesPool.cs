using UnityEngine;
using UnityEngine.Pool;

namespace Source.Scripts
{
    public class CubesPool : MonoBehaviour
    {
        [SerializeField] private Cube _cubePrefab;

        private ObjectPool<Cube> _pool;
        private CubeFactory _cubeFactory;

        private void Start()
        {
            _cubeFactory = new(_cubePrefab);

            _pool = new(
                createFunc: CreateCube,
                actionOnGet: OnTakeFromPool,
                actionOnRelease: OnReturnToPool,
                actionOnDestroy: OnDestroyCube,
                collectionCheck: false
            );
        }

        public Cube Take()
        {
            return _pool.Get();
        }

        private void OnTakeFromPool(Cube cube)
        {
            cube.gameObject.SetActive(true);
        }

        private void OnReturnToPool(Cube cube)
        {
            cube.gameObject.SetActive(false);
        }

        private void OnDestroyCube(Cube cube)
        {
            cube.ActiveLifeFinished -= Release;
            
            Destroy(cube.gameObject);
        }

        private void Release(Cube cube)
        {
            _pool.Release(cube);
        }

        private Cube CreateCube()
        {
            Cube newCube = _cubeFactory.ConstructCube();

            newCube.ActiveLifeFinished += Release;
            
            return newCube;
        }
    }
}