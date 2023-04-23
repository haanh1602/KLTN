using System;
using System.Collections;
using System.Collections.Generic;
using An.Optimization;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestCauTrucDuLieuPhuHop : MonoBehaviour
{
    public int amount = 1;
    public BasePlayer Player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            for (int i = 0; i < amount; i++)
            {
                string randomId = (Random.Range(0, EnemySpawner.EnemyCount)).ToString();
                An.Optimization.EnemyManager.Instance.GetEnemyByIdDictionary(randomId);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            for (int i = 0; i < amount; i++)
            {
                string randomId = (Random.Range(0, EnemySpawner.EnemyCount)).ToString();
                An.Optimization.EnemyManager.Instance.GetEnemyByIdList(randomId);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            for (int i = 0; i < amount; i++)
            {
                An.Optimization.EnemyManager.Instance.GetNearestEnemyDictionary(Player.transform.position);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            for (int i = 0; i < amount; i++)
            {
                An.Optimization.EnemyManager.Instance.GetNearestEnemyList(Player.transform.position);
            }
        }*/
    }
}
