using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] Camera cam;
    private Vector3 topRight;
    private Vector3 bottomLeft;
    public const int MAX_ENEMY = 10;
    
    private int time;

    public long hpWave = 500;
    public float timeWave = 5;
    int wave = 0;
    float bonusHpWave = .1f;

    public static SpawnEnemy _ins;

    Coroutine spawnCor;
    private void Awake()
    {
        _ins = this;
    }
    void Start()
    {
        time = 0;
        topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        spawnCor = StartCoroutine(CoroutineSpawn());
    }

    void Update()
    {
        time++;
        topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
    }

    void Spawn()
    {
        //hpWave = (long)(BasePlayer._ins.playerAttack.GetDPS() * timeWave*(1+wave*bonusHpWave));

        //bonusHpWave *= 1.1f;
        //int countEnemy = EnemyManager._ins.listBaseEnemy.Count;
        //int randomNumber = UnityEngine.Random.Range(0, countEnemy);
        //int numberEnemy = (int)(hpWave/ EnemyManager._ins.listBaseEnemy[randomNumber].maxHp);

        //float ratioHp = 1f;
        //if (numberEnemy > 100)
        //{
        //    ratioHp = numberEnemy / 100f;
        //    numberEnemy = 100;
        //    EnemyManager._ins.listBaseEnemy[randomNumber].maxHp = (long)(EnemyManager._ins.listBaseEnemy[randomNumber].maxHp * ratioHp);
        //}

        //EnemyManager._ins.listBaseEnemy[randomNumber].movementSpeed = EnemyManager._ins.listBaseEnemy[randomNumber].movementSpeed*(1 + bonusHpWave);

        


        float minX = bottomLeft.x - 1, maxX = bottomLeft.x, minY = bottomLeft.y - 1, maxY = bottomLeft.y;
        switch ((int)UnityEngine.Random.Range(0, 4))
        {
            case 0:
                minX = bottomLeft.x - 5;
                maxX = bottomLeft.x;
                minY = topRight.y;
                maxY = topRight.y + 5;
                break;
            case 1:
                minX = topRight.x;
                maxX = topRight.x + 5;
                minY = topRight.y;
                maxY = topRight.y + 5;
                break;
            case 2:
                minX = topRight.x;
                maxX = topRight.x + 5;
                minY = bottomLeft.y - 5;
                maxY = topRight.y;
                break;
            case 3:
                minX = bottomLeft.x - 5;
                maxX = bottomLeft.x;
                minY = bottomLeft.y - 5;
                maxY = bottomLeft.y;
                break;
        }

        //if (numberEnemy < 20)
        //{
        //    for (int i = 0; i < numberEnemy; i++)
        //    {
        //        BaseEnemy e = EnemyManager._ins.GetEnemy(EnemyManager._ins.listBaseEnemy[randomNumber]);
        //        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
        //        e.gameObject.transform.position = randomPosition;
        //        e.gameObject.SetActive(true);
        //        EnemyManager._ins.listAliveEnemy.Add(e);
        //        //e.transform.ResetPosition();
        //    }
        //}
        //else
        //{
        //    for (int i = 0; i < numberEnemy; i++)
        //    {
        //        BaseEnemy e = EnemyManager._ins.GetEnemy(EnemyManager._ins.listBaseEnemy[randomNumber]);
        //        Vector3 randCir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

        //        randCir.Normalize();

        //        float distance = Random.Range(10f, 11f);

        //        Vector3 randomPosition = BasePlayer._ins.transform.position+randCir*distance;
        //        e.gameObject.transform.position = randomPosition;
        //        e.gameObject.SetActive(true);
        //        EnemyManager._ins.listAliveEnemy.Add(e);
        //        //e.transform.ResetPosition();
        //    }
        //}
        if (wave % 5 == 0)
        {
            BaseEnemy boss = EnemyManager._ins.GetEnemy(EnemyManager._ins.boss);
            Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
            boss.gameObject.transform.position = randomPosition;
            boss.gameObject.SetActive(true);
            EnemyManager._ins.listAliveEnemy.Add(boss);
        }
        if (wave % 3 == 0)
        {
            BaseEnemy boss = EnemyManager._ins.GetEnemy(EnemyManager._ins.boss2);
            Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
            boss.gameObject.transform.position = randomPosition;
            boss.gameObject.SetActive(true);
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
            wave++;
            Spawn();

            yield return new WaitForSeconds(10f);
            

        }
        
    }
}
