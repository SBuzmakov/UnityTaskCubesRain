using System.Collections.Generic;
using Source.Scripts.Objects;
using Source.Scripts.Spawners;
using UnityEngine;

namespace Source.Scripts.Services
{
    public class Exploder : MonoBehaviour
    {
        private const int MaxExplodableCollidersCount = 200;
        
        [SerializeField] private float _explosionRadius;
        [SerializeField] private float _explosionForce;
        [SerializeField] private BombSpawner _bombSpawner;
        
        public void Explode(Bomb bomb)
        {
            foreach (Rigidbody explodableCube in GetExplodableCubes(bomb))
            {
                explodableCube.AddExplosionForce(_explosionForce, bomb.transform.position, _explosionRadius);
            }
        }

        private List<Rigidbody> GetExplodableCubes(Bomb bomb)
        {
            Collider[] results = new Collider[MaxExplodableCollidersCount];
            
            int size = Physics.OverlapSphereNonAlloc(bomb.transform.position, _explosionRadius, results);
            
            List<Rigidbody> explodableCubes = new List<Rigidbody>();

            for (var i = 0; i < size; i++)
            {
                if (results[i].attachedRigidbody != null)
                {
                    explodableCubes.Add(results[i].attachedRigidbody);
                }
            }

            return explodableCubes;
        }
    }
}
