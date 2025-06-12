using Source.Scripts.Objects;
using Source.Scripts.Spawners;
using Source.Scripts.UI;
using UnityEngine;

namespace Source.Scripts.Services
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private CubeSpawner _cubeSpawner;
        [SerializeField] private BombSpawner _bombSpawner;
        [SerializeField] private Cube _cubePrefab;
        [SerializeField] private Bomb _bombPrefab;
        [SerializeField] private CubesPool _cubesPool;
        [SerializeField] private BombsPool _bombsPool;
        [SerializeField] private Exploder _exploder;
        [SerializeField] private Painter _painter;
        [SerializeField] private ObjectsCounter<Cube> _cubesCounter;
        [SerializeField] private ObjectsCounter<Bomb> _bombCounter;
        [SerializeField] private CountViewPresenter<Cube> _cubesCountViewPresenter;
        [SerializeField] private CountViewPresenter<Bomb> _bombsCountViewPresenter;
        
        private ObjectFactory<Cube> _cubeFactory;
        private ObjectFactory<Bomb> _bombFactory;

        private void Awake()
        {
            _cubeFactory = new ObjectFactory<Cube>(_cubePrefab, _painter, null);
            _bombFactory = new ObjectFactory<Bomb>(_bombPrefab, _painter, _exploder);
            
            _cubesPool.Initialize(_cubeFactory);
            _bombsPool.Initialize(_bombFactory);
            
            _cubeSpawner.Initialize(_cubesPool);
            _bombSpawner.Initialize(_bombsPool, _cubesPool);
            
            _cubesCountViewPresenter.Initialize();
            _bombsCountViewPresenter.Initialize();
        }
    }
}