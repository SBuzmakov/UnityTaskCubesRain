using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Scripts.Objects
{
    public class Bomb : MonoBehaviour, ISpawnable<Bomb>
    {
        private const float MinLifeTimeAfterCollision = 2.0f;
        private const float MaxLifeTimeAfterCollision = 5.0f;

        private MeshRenderer _renderer;
        private Coroutine _coroutine;

        public event Action Spawned;
        public event Action<Bomb> ActiveLifeFinished;
        public event Action<Bomb> Destroyed;
        public event Action<Renderer> NeedsBaseColor;
        public event Action<float, MeshRenderer> ActiveLifeStarted;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        public void SetPosition(Vector3 pos)
        {
            transform.position = pos;
        }

        public void InvokeActiveLife()
        {
            Spawned?.Invoke();
            NeedsBaseColor?.Invoke(_renderer);
        }

        public void StartActiveLife()
        {
            float lifeTime = Random.Range(MinLifeTimeAfterCollision, MaxLifeTimeAfterCollision);
            
            ActiveLifeStarted?.Invoke(lifeTime, _renderer);
            
            if (_coroutine == null)
                _coroutine = StartCoroutine(LifeTimerJob(lifeTime));
        }

        private IEnumerator LifeTimerJob(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);

            _coroutine = null;
            
            ActiveLifeFinished?.Invoke(this);
        }
        
        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}
