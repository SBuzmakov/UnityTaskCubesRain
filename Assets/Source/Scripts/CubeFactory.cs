using System;
using Object = UnityEngine.Object;

namespace Source.Scripts
{
    public class CubeFactory
    {
        private readonly Cube _cubePrefab;
        private readonly Painter _painter;
        
        public CubeFactory(Cube cubePrefab)
        {
            _cubePrefab = cubePrefab;
            
            _painter = new();
        }

        public Cube ConstructCube()
        {
            Cube cube = Object.Instantiate(_cubePrefab);

            if (cube != null)
            {
                CubeListener cubeListener = new CubeListener(cube, _painter);

                cubeListener.Subscribe();
                
                return cube;
            }

            throw new NullReferenceException("cube component is null");
        }
    }
}