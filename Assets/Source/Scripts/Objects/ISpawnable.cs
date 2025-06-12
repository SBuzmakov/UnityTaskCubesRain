using System;

namespace Source.Scripts.Objects
{
    public interface ISpawnable<T> where T : UnityEngine.Object
    {
        event Action<T> ActiveLifeFinished;
    }
}