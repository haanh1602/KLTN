using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

public class SpawnEnemy : MonoBehaviour
{
    public const int MAX_ENEMY = 10;

    private int wave = 0;

    public static SpawnEnemy _ins;
    public int numberEnemy = 1;
    Coroutine spawnCor;

    private void Awake()
    {
        _ins = this;
    }
    void Start()
    {
        ForceNextWave();
    }

    void Spawn()
    {
        int randomNumber = UnityEngine.Random.Range(0, EnemyManager._ins.listBaseEnemy.Count);
        if (EnemyManager._ins.listAliveEnemy.Count < 10)
        {
            for (int i = 0; i < numberEnemy; i++)
            {
                BaseEnemy e = EnemyManager._ins.GetEnemy(EnemyManager._ins.listBaseEnemy[randomNumber]);
                RandomPosition.ReSpawnObjectRandom(e.gameObject);
                e.ResetPool();
                EnemyManager._ins.listAliveEnemy.Add(e);
            }
        }
         
        if (wave % 5 == 0)
        {
            BaseEnemy boss = EnemyManager._ins.GetEnemy(EnemyManager._ins.boss);
            RandomPosition.ReSpawnObjectRandom(boss.gameObject);
            boss.ResetPool();
            EnemyManager._ins.listAliveEnemy.Add(boss);
        }
        if (wave % 3 == 0)
        {
            BaseEnemy boss = EnemyManager._ins.GetEnemy(EnemyManager._ins.boss2);
            RandomPosition.ReSpawnObjectRandom(boss.gameObject);
            boss.ResetPool();
            EnemyManager._ins.listAliveEnemy.Add(boss);
        }


    }


    public void ForceNextWave()
    {
        if (spawnCor != null)
        {
            StopCoroutine(spawnCor);
        }
        spawnCor = StartCoroutine(CoroutineSpawn());
    }

    private IEnumerator CoroutineSpawn()
    {
        while (true)
        {
            yield return new WaitUntil(() => !EnemyManager._ins.IsPause);
            
            wave++;
            wave %= 15;
            Spawn();
            yield return new WaitForSeconds(10f);
            

        }
        
    }
}
