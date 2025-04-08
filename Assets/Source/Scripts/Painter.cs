using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Scripts
{
    public class Painter
    {
        private readonly Color _baseColor = Color.white;
    
        public void RandomRepaint(Renderer objectRenderer)
        {
            if (objectRenderer == false) 
                throw new NullReferenceException("objectRenderer is null");
        
            Color randomColor = new Color(Random.value, Random.value, Random.value);
            objectRenderer.material.color = randomColor;
        }

        public void BaseRepaint(Renderer objectRenderer)
        {
            if (objectRenderer == false)
                throw new NullReferenceException("objectRenderer or color is null");
        
            objectRenderer.material.color = _baseColor;
        }
    }
}
