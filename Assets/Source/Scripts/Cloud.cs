using System.Collections;
using Source.Scripts;
using Unity.VisualScripting;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private GameObject _ground;
    [SerializeField] private CubesPool _cubesPool;
    [SerializeField] private float _cloudHeight;
    [SerializeField, Range(4, 6)] private float _cloudDivisor;
    [SerializeField, Range(1, 10)] private int _rainIntensity;
    [SerializeField, Range(0, 1)] private float _rainFrequency;
    [SerializeField] private bool _isWorking = true;

    private Coroutine _coroutine;
    private float _positionY;

    private void Start()
    {
        _positionY = _ground.transform.position.y + _cloudHeight;

        _coroutine = StartCoroutine(Rain());
    }

    private void OnDestroy()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator Rain()
    {
        WaitForSeconds wait = new WaitForSeconds(_rainFrequency);

        while (_isWorking)
        {
            GetCubes();

            yield return wait;
        }
    }

    private void GetCubes()
    {
        for (int i = 0; i < _rainIntensity; i++)
        {
            GameObject cube = _cubesPool.GetCube();

            cube.transform.SetPositionAndRotation(GetCubePosition(), Quaternion.identity);

            cube.SetActive(true);
        }
    }

    private Vector3 GetCubePosition()
    {
        Vector3 center = _ground.GetComponent<Collider>().bounds.center;

        Vector3 size = _ground.GetComponent<Collider>().bounds.size;

        return new Vector3(Random.Range(center.x - size.x / _cloudDivisor, center.x + size.x / _cloudDivisor),
            _positionY,
            Random.Range(center.x - size.x / _cloudDivisor, center.x + size.x / _cloudDivisor));
    }
}