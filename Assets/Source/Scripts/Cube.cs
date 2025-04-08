using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Scripts
{
    public class Cube : MonoBehaviour
    {
        private const float MinLifeTimeAfterCollision = 2.0f;
        private const float MaxLifeTimeAfterCollision = 5.0f;

        private Renderer _renderer;
        private Coroutine _lifeTimeCoroutine;
        private bool _isCollisioned;

        public event Action<Renderer> TouchedPlatform;
        public event Action Destroyed;
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

                StartCoroutine(LifeTimerJob(GetRandomLifeTime()));

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

            ActiveLifeFinished?.Invoke(this);

            NeedsBaseColor?.Invoke(_renderer);
        }

        private float GetRandomLifeTime()
        {
            return Random.Range(MinLifeTimeAfterCollision, MaxLifeTimeAfterCollision);
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke();
        }
    }
}