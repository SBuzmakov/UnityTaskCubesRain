using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField, Range(2, 5)] private float _lifeTimeAfterCollision;

    private Color _baseRendererColor;
    
    public event Action<Renderer> TouchedPlatform;
    public event Action<Renderer, Color> TurnedOff;
    public event Action Destroyed;

    public bool IsCollisioned { get; private set; } = false;

    private void Start()
    {
        _baseRendererColor = gameObject.GetComponent<Renderer>().material.color;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Platform>() && IsCollisioned == false)
        {
            IsCollisioned = true;
            
            Invoke(nameof(SetActiveFalse), _lifeTimeAfterCollision);

            TouchedPlatform?.Invoke(gameObject.GetComponent<Renderer>());
        }
    }

    private void OnDestroy()
    {
        Destroyed?.Invoke();
    }
    
    private void SetActiveFalse()
    {
        gameObject.SetActive(false);

        IsCollisioned = false;
        
        TurnedOff?.Invoke(gameObject.GetComponent<Renderer>(), _baseRendererColor);
    }
}