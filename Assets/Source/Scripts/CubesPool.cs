using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts
{
    public class CubesPool : MonoBehaviour
    {
        [SerializeField] private GameObject _cubePrefab;
        [SerializeField, Range(1, 100)] public int _poolSize;
        [SerializeField] Painter _painter;

        private List<GameObject> _pool = new();

        private void Start()
        {
            Create();
        }

        public GameObject GetCube()
        {
            foreach (GameObject cube in _pool)
            {
                if (cube.activeInHierarchy == false)
                {
                    return cube;
                }
            }

            return null;
        }

        private void Create()
        {
            for (int i = 0; i < _poolSize; i++)
            {
                CreateCube();
            }
        }

        private void CreateCube()
        {
            GameObject newCubeObject = Instantiate(_cubePrefab);

            Cube cube = newCubeObject.GetComponent<Cube>();
            
            if (cube != null)
            {
                CubeListener cubeListener = new CubeListener(cube, _painter);

                cubeListener.Subscribe();
            }
            else
            {
                throw new NullReferenceException("cube component is null");
            }
            
            newCubeObject.SetActive(false);

            _pool.Add(newCubeObject);
        }
    }
}