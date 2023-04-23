using System;
using System.Collections;
using System.Collections.Generic;
using An.Optimization;
using UnityEngine;
using Random = UnityEngine.Random;

namespace An.Optimization
{
    public class SpawnFlower : MonoBehaviour
    {
        [SerializeField] private Flower flowerPrefab;

        public int spawnDistance = 4;
        public float padding = 0.5f;
        public Vector2Int bottomLeft = new Vector2Int(-30, -30);
        public Vector2Int topRight = new Vector2Int(30, 30);

        private List<Flower> _flowers = new List<Flower>();
    
        // Start is called before the first frame update
        void Start()
        {
            for (int i = (int) bottomLeft.x; i < topRight.x; i += spawnDistance)
            {
                for (int j = (int) bottomLeft.y; j < topRight.y; j += spawnDistance)
                {
                    Vector2 spawnPosition = new Vector2
                    {
                        x = i + Random.Range(padding, spawnDistance - padding),
                        y = j + Random.Range(padding, spawnDistance - padding)
                    };
                    Flower tempFlower = Instantiate(flowerPrefab, spawnPosition, Quaternion.identity, transform);
                    _flowers.Add(tempFlower);
                }
            }
        }
    }

}
