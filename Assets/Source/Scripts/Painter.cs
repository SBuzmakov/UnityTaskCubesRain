using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Painter : MonoBehaviour
{
    public void RandomRepaint(Renderer objectRenderer)
    {
        if (objectRenderer == false) 
            throw new NullReferenceException("objectRenderer is null");
        
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        objectRenderer.material.color = randomColor;
    }

    public void Repaint(Renderer objectRenderer, Color color)
    {
        if (objectRenderer == false)
            throw new NullReferenceException("objectRenderer or color is null");
        
        objectRenderer.material.color = color;
    }
}
