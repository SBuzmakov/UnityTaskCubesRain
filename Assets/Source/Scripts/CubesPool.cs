using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts
{
    public class CubesPool : MonoBehaviour
    {
        [SerializeField] private GameObject _cubePrefab;
        [SerializeField] private int _poolSize = 100;

        private List<GameObject> pool = new();

        private void Start()
        {
            Create();
        }

        private void Create()
        {
            for (int i = 0; i < _poolSize; i++)
            {
                GameObject newCube = Instantiate(_cubePrefab);
                newCube.SetActive(false);
                pool.Add(newCube);
            }
        }
    }
}
