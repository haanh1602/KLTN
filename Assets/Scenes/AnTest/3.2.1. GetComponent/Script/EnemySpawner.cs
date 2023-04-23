using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ReSharper disable MemberCanBePrivate.Global

namespace An.Optimization
{
    public class EnemySpawner : MonoBehaviour
    {
        public Enemy enemyPrefab;

        public static int AwakeType = 0;

        private List<Enemy> spawnedEnemies = new List<Enemy>();
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                AwakeType = 0;
                SpawnEnemies();
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                AwakeType = 1;
                SpawnEnemies();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                AwakeType = 2;
                SpawnEnemies();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                for (int i = 0; i < spawnedEnemies.Count; i++) {
                    Destroy(spawnedEnemies[i].gameObject);
                }
                spawnedEnemies.Clear();
                EnemyManager.Instance.EnemiesList.Clear();
                EnemyManager.Instance.EnemiesDic.Clear();
            }
        }

        public int spawn = 30;
        
        // 4.2.1.5
        /*public void SpawnEnemies()
        {
            StartCoroutine(IESpawnEnemy());
        }
        
        IEnumerator IESpawnEnemy()
        {
            for (int i = 0; i < spawn; i++)
            {
                var enemy = Instantiate(enemyPrefab, transform);
                spawnedEnemies.Add(enemy);
                yield return null;
            }
        }*/
        
        // 4.2.1.6
        public void SpawnEnemies()
        {
            StartCoroutine(IESpawnEnemy());
        }
        
        IEnumerator IESpawnEnemy()
        {
            for (int i = 0; i < spawn; i++)
            {
                var enemy = Instantiate(enemyPrefab, 
                    new Vector3(Random.Range(-30f, 30f), Random.Range(-30f, 30f)), 
                    Quaternion.identity, 
                    transform);
                spawnedEnemies.Add(enemy);
                EnemyManager.Instance.Add(enemy);
                yield return null;
            }
        }
        
        // 4.2.1.8
        /*public void SpawnEnemies()
        {
            StartCoroutine(IESpawnEnemy());
        }

        public static int EnemyCount = 0;
        
        IEnumerator IESpawnEnemy()
        {
            for (int i = 0; i < spawn; i++)
            {
                var enemy = Instantiate(enemyPrefab, transform);
                enemy.id = (EnemyCount++).ToString();
                EnemyManager.Instance.Add(enemy);
                yield return null;
            }
        }*/
        
        /*public void SpawnEnemies()
        {
            for (int i = 0; i < spawn; i++)
            {
                var enemy = Instantiate(enemyPrefab, transform);
                spawnedEnemies.Add(enemy);
            }
        }*/
        
        // 4.2.1.9
        /*public void SpawnEnemies()
        {
            for (int i = 0; i < spawn; i++)
            {
                var enemy = Instantiate(enemyPrefab);
                enemy.transform.SetParent(EnemyManager.Instance.transform);
                EnemyManager.Instance.Add(enemy);
            }
        }*/
        
        /*public void SpawnEnemies()
        {
            for (int i = 0; i < spawn; i++)
            {
                var enemy = Instantiate(enemyPrefab, parent: EnemyManager.Instance.transform);
                EnemyManager.Instance.Add(enemy);
            }
        }*/
    }

}
