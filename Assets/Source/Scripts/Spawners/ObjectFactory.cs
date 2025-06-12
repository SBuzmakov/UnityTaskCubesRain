using Source.Scripts.Objects;
using Source.Scripts.Services;
using UnityEngine;

namespace Source.Scripts.Spawners
{
    public class ObjectFactory<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly Painter _painter;
        private readonly Exploder _exploder;

        public ObjectFactory(T prefab, Painter painter, Exploder exploder)
        {
            _prefab = prefab;
            _painter = painter;
            _exploder = exploder;
        }

        public T Create()
        {
            T obj = Object.Instantiate(_prefab);
            
            if (obj.TryGetComponent(out Cube cube))
            {
                CubeListener listener = new CubeListener(cube, _painter);
                listener.Subscribe();

                return obj;
            }

            if (obj.TryGetComponent(out Bomb bomb))
            {
                BombListener listener = new BombListener(bomb, _painter, _exploder);
                listener.Subscribe();

                return obj;
            }

            return null;
        }
    }
}