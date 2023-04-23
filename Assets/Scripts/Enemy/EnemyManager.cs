using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager _ins;

    public Transform transformCache;
    public List<BaseEnemy> listBaseEnemy;
    public List<BaseEnemy> listAliveEnemy;
    public List<List<BaseEnemy>> listEnemyPool;
    public BaseEnemy boss;
    public BaseEnemy boss2;

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

        listEnemyPool = new List<List<BaseEnemy>>();
        for(int i = 0; i < listBaseEnemy.Count; i++)
        {
            GameObject enemyObj = Instantiate(listBaseEnemy[i].gameObject, transformCache);
            enemyObj.SetActive(false);
            BaseEnemy enemyCache = enemyObj.GetComponent<BaseEnemy>();
            listBaseEnemy[i]= enemyCache;
            List<BaseEnemy> listEnemy = new List<BaseEnemy>();
            listEnemyPool.Add(listEnemy);
        }
    }

    public BaseEnemy GetEnemy(BaseEnemy e)
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
            listEnemyPool.Add(newList);
        }

        return result;
    }

    
    public BaseEnemy GetEnemy(int id)
    {
        BaseEnemy result = null;

       
        if (listEnemyPool[id].Count > 0)
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
        }
        
        return result;
    }

    public void AddToPoolEnemy(BaseEnemy enemy)
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
        if (index !=-1)
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
        }
    }

    public void DeSpawnActiveEnemy(bool isRightAnswer)
    {
        activeEnemy.Die(isRightAnswer);
    }

    public BaseEnemy GetNearestEnemyList(Vector3 playerPosition, out float sqrDistance)
    {
        sqrDistance = 0f;
        int index = -1;
        float min = 1000000f;
        Debug.Log(listAliveEnemy.Count);
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
