using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Scripts.Objects
{
    public class Cube : MonoBehaviour, ISpawnable<Cube>
    {
        private const float MinLifeTimeAfterCollision = 2.0f;
        private const float MaxLifeTimeAfterCollision = 5.0f;

        private Renderer _renderer;
        private Coroutine _coroutine;
        private bool _isCollisioned;

        public event Action<Renderer> TouchedPlatform;
        public event Action<Cube> Destroyed;
        public event Action<Cube> ActiveLifeFinished;
        public event Action<Renderer> NeedsBaseColor;

        private void Start()
        {
            _renderer = GetComponent<Renderer>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Platform _) && _isCollisioned == false)
            {
                _isCollisioned = true;
                float lifeTime = Random.Range(MinLifeTimeAfterCollision, MaxLifeTimeAfterCollision);

                if (_coroutine == null)
                    _coroutine = StartCoroutine(LifeTimerJob(lifeTime));

                TouchedPlatform?.Invoke(_renderer);
            }
        }

        private void OnDisable()
        {
            _isCollisioned = false;
        }

        private IEnumerator LifeTimerJob(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);

            _coroutine = null;

            ActiveLifeFinished?.Invoke(this);
            NeedsBaseColor?.Invoke(_renderer);
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}