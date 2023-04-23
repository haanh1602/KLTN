using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace An.Optimization
{
    public class EnemyManager : Singleton<EnemyManager>
    {
        public List<Enemy> EnemiesList = new List<Enemy>();
        public Dictionary<string, Enemy> EnemiesDic = new Dictionary<string, Enemy>();

        public List<Transform> EnemiesTransformsList = new List<Transform>();

        private static BasePlayer _player;

        private static BasePlayer Player
        {
            get
            {
                if (_player == null)
                    _player = FindObjectOfType<BasePlayer>();
                return _player;
            }
        }

        private const float maxUsableDistance = 10f;

        public int test42112 = 1000;
        
        private void Update()
        {
            // 4.2.1.11
            //DisableUnusableEnemies();
            
            // 4.2.1.12
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                for (int i = 0; i < test42112; i++)
                {
                    float sqrdDistance = (EnemiesList[0].transform.position - Vector3.zero).sqrMagnitude;
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                for (int i = 0; i < test42112; i++)
                {
                    float sqrdDistance = (EnemiesList[0].transform.position - Vector3.zero).magnitude;
                }
            }
        }
        
        void DisableUnusableEnemies()
        {
            for (int i = 0; i < EnemiesList.Count; i++)
            {
                if (Vector2.Distance(EnemiesList[i].transform.position, Player.transform.position) 
                    < maxUsableDistance)
                {
                    EnemiesList[i].gameObject.SetActive(true);
                }
                else
                {
                    EnemiesList[i].gameObject.SetActive(false);
                }
            }
        }

        public Enemy GetEnemyByIdDictionary(string id)
        {
            return EnemiesDic[id];
        }

        public Enemy GetEnemyByIdList(string id)
        {
            for (int i = 0; i < EnemiesList.Count; i++)
            {
                if (EnemiesList[i].id.Equals(id)) 
                    return EnemiesList[i];
            }
            return null;
        }

        /*public Enemy GetNearestEnemyList(Vector3 playerPosition)
        {
            int index = -1;
            float min = 1000000f;
            for (int i = 0; i < EnemiesList.Count; i++)
            {
                float sqrdDistance = (EnemiesList[i].transform.position - playerPosition).sqrMagnitude;
                if (sqrdDistance < min)
                {
                    index = i;
                    min = sqrdDistance;
                }
            }
            if (index < 0) return null;
            return EnemiesList[index];
        }*/
        
        // 4.2.1.12
        private int typeGetNearest = 0;
        public Enemy GetNearestEnemyList(Vector3 playerPosition)
        {
            int index = -1;
            float min = 1000000f;
            for (int i = 0; i < EnemiesList.Count; i++)
            {
                if (typeGetNearest == 0)
                {
                    float sqrdDistance = (EnemiesList[i].transform.position - playerPosition).sqrMagnitude;
                    if (sqrdDistance < min)
                    {
                        index = i;
                        min = sqrdDistance;
                    }
                }
                else
                {
                    float distance = (EnemiesList[i].transform.position - playerPosition).magnitude;
                    if (distance < min)
                    {
                        index = i;
                        min = distance;
                    }
                }
                
            }
            if (index < 0) return null;
            return EnemiesList[index];
        }

        public Enemy GetNearestEnemyDictionary(Vector3 playerPosition)
        {
            string id = "";
            float min = 1000000f;
            foreach (var enemy in EnemiesDic)
            {
                if (Vector3.Distance(enemy.Value.transform.position, playerPosition) < min)
                    id = enemy.Key;
            }

            if (string.IsNullOrEmpty(id)) return null;
            return EnemiesDic[id];
        }

        public void Add(Enemy newEnemy)
        {
            EnemiesList.Add(newEnemy);
            if (!EnemiesDic.ContainsKey(newEnemy.id)) 
                EnemiesDic.Add(newEnemy.id, newEnemy);
            EnemiesTransformsList.Add(newEnemy.transform);
        }
    }
}
