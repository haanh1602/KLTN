using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryHandle : MonoBehaviour
{
    public GameObject spawnPrefab;

    [Space(20)] 
    public int quantity = 10;
    
    public Button spawnButton;

    private void Awake()
    {
        spawnButton.onClick.AddListener(SpawnObject);
    }

    private void SpawnObject()
    {
        for (int i = 0; i < quantity; i++)
        {
            Instantiate(spawnPrefab, 
                new Vector3(UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(-3f, 3f)), 
                new Quaternion(UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f), spawnPrefab.transform.rotation.w));
        }
    }
}
