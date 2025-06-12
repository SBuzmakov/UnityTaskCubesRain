using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Scripts.Services
{
    public class Painter : MonoBehaviour
    {
        private readonly Color _whiteColor = Color.white;
        private readonly Color _blackColor = Color.black;
        private Coroutine _coroutine;

        public void RandomRepaint(Renderer objectRenderer)
        {
            if (objectRenderer == false)
                throw new NullReferenceException("objectRenderer is null");

            Color randomColor = new Color(Random.value, Random.value, Random.value);
            objectRenderer.material.color = randomColor;
        }

        public void RepaintWhite(Renderer objectRenderer)
        {
            if (objectRenderer == false)
                throw new NullReferenceException("objectRenderer or color is null");

            objectRenderer.material.color = _whiteColor;
        }

        public void RepaintBlack(Renderer objectRenderer)
        {
            if (objectRenderer == false)
                throw new NullReferenceException("objectRenderer or color is null");

            objectRenderer.material.color = _blackColor;
        }

        public void ChangeAlpha(float lifeTime, MeshRenderer objectRenderer)
        {
            StartCoroutine(ChangeAlphaJob(lifeTime, objectRenderer));
        }

        private IEnumerator ChangeAlphaJob(float lifeTime, MeshRenderer objectRenderer)
        {
            float elapsed = 0f;
            Color color = objectRenderer.material.color;
            
            while (elapsed < lifeTime)
            {
                elapsed += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, elapsed / lifeTime);
                color.a = alpha;
                objectRenderer.material.color = color;
                yield return null;
            }

            color.a = 0f;
            objectRenderer.material.color = color;
        }
    }
}