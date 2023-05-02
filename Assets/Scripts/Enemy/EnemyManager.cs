using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager _ins;

    public Transform transformCache;
    public List<BaseEnemy> listBaseEnemy = new List<BaseEnemy>();
    public List<BaseEnemy> listAliveEnemy = new List<BaseEnemy>();

    public BaseEnemy activeEnemy = null;

    private int totalEnemy = 0;
    private bool pause = false;

    public bool IsPause => pause;

    public void PauseEnemy()
    {
        if (pause != true)
        {
            
            for (int i = 0; i < listAliveEnemy.Count; i++)
            {
                listAliveEnemy[i].StopMove();
            }
        }
        pause = true;
    }

    public void ContinueEnemy()
    {
        pause = false;
    }
    
    public void Awake()
    {
        _ins = this;

        /*listEnemyPool = new List<List<BaseEnemy>>();
        for(int i = 0; i < listBaseEnemy.Count; i++)
        {
            GameObject enemyObj = Instantiate(listBaseEnemy[i].gameObject, transformCache);
            enemyObj.SetActive(false);
            BaseEnemy enemyCache = enemyObj.GetComponent<BaseEnemy>();
            listBaseEnemy[i]= enemyCache;
            List<BaseEnemy> listEnemy = new List<BaseEnemy>();
            listEnemyPool.Add(listEnemy);
        }*/
    }

    public void SpawnEnemy(List<QuestionData> questionData)
    {
        StartCoroutine(IESpawnEnemy(questionData));
    }

    IEnumerator IESpawnEnemy(List<QuestionData> questionData)
    {
        List<BaseEnemy> hardEnemies = new List<BaseEnemy>();
        List<BaseEnemy> mediumEnemies = new List<BaseEnemy>();
        List<BaseEnemy> easyEnemies = new List<BaseEnemy>();

        for (int i = 0; i < listBaseEnemy.Count; i++)
        {
            switch (listBaseEnemy[i].level)
            {
                case QuestionLevel.Hard:
                    hardEnemies.Add(listBaseEnemy[i]);
                    break;
                case QuestionLevel.Medium:
                    mediumEnemies.Add(listBaseEnemy[i]);
                    break;
                case QuestionLevel.Easy:
                    easyEnemies.Add(listBaseEnemy[i]);
                    break;
            }
        }

        for (int i = 0; i < questionData.Count; i++)
        {
            BaseEnemy enemy = null;
            switch (questionData[i].QuestLevel())
            {
                case QuestionLevel.Hard:
                    enemy = hardEnemies[UnityEngine.Random.Range(0, hardEnemies.Count)];
                    break;
                case QuestionLevel.Medium:
                    enemy = mediumEnemies[UnityEngine.Random.Range(0, mediumEnemies.Count)];
                    break;
                case QuestionLevel.Easy:
                    enemy = easyEnemies[UnityEngine.Random.Range(0, easyEnemies.Count)];
                    break;
            }
            if (enemy == null) continue;

            Vector2 spawnPosition = new Vector2();
            spawnPosition.x = (UnityEngine.Random.Range(0, 2) == 0? 1 : -1) * UnityEngine.Random.Range(2.5f, 25f);
            spawnPosition.y = (UnityEngine.Random.Range(0, 2) == 0? 1 : -1) * UnityEngine.Random.Range(2.5f, 25f);
            
            var newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity, transform);
            listAliveEnemy.Add(newEnemy);
            yield return null;
        }
    }

    #region Obsolete

        /*public BaseEnemy GetEnemy(BaseEnemy e)
    {
        BaseEnemy result=null;
        for (int i = 0;i< listBaseEnemy.Count; i++)
        {
            if (e.poolIndex == listBaseEnemy[i].poolIndex)
            {
                result = GetEnemy(i);
                break;
            }
        }

        if (result == null) // trong pool ko có loại quái cần, sinh quái mới và cho vào pool
        {
            GameObject enemyObj= Instantiate(e.gameObject, transformCache);
            result = enemyObj.GetComponent<BaseEnemy>();
            listBaseEnemy.Add(result);
            List<BaseEnemy> newList = new List<BaseEnemy>();
            newList.Add(result);
            //listEnemyPool.Add(newList);
        }

        return result;
    }*/
    
    /*public BaseEnemy GetEnemy(int id)
    {
        BaseEnemy result = null;

       
        /*if (listEnemyPool[id].Count > 0)
        {
            result = listEnemyPool[id][0];
            listEnemyPool[id].RemoveAt(0);
            result.ResetPool();
        }
        else
        {
            GameObject enemyObj = Instantiate(listBaseEnemy[id].gameObject, transformCache);
            result = enemyObj.GetComponent<BaseEnemy>();
            listAliveEnemy.Add(result);
        }#1#
        
        return result;
    }*/

    /*public void AddToPoolEnemy(BaseEnemy enemy)
    {
        int index = -1;
        for (int i = 0; i < listBaseEnemy.Count; i++)
        {
            if (enemy.poolIndex == listBaseEnemy[i].poolIndex)
            {
                index = i;
                break;
            }
        }
        /*if (index !=-1)
        {
            listEnemyPool[index].Add(enemy);
           
            enemy.gameObject.SetActive(false);
            
        }
        else
        {
            GameObject enemyObj = enemy.gameObject;
            listBaseEnemy.Add(enemy);
            //
            List<BaseEnemy> newList = new List<BaseEnemy>();
            newList.Add(enemy);
            listEnemyPool.Add(newList);
        }#1#
    }*/

    #endregion

    public void DeSpawnActiveEnemy(bool isRightAnswer)
    {
        activeEnemy.Die(isRightAnswer);
    }

    public BaseEnemy GetNearestEnemyList(Vector3 playerPosition, out float sqrDistance)
    {
        sqrDistance = 0f;
        int index = -1;
        float min = 1000000f;
        for (int i = 0; i < listAliveEnemy.Count; i++)
        {
            float sqrdDistance = (listAliveEnemy[i].transform.position - playerPosition).sqrMagnitude;
            if (sqrdDistance < min)
            {
                index = i;
                min = sqrdDistance;
                sqrDistance = sqrdDistance;
            }
        }
        if (index < 0) return null;
        return listAliveEnemy[index];
    }
}
