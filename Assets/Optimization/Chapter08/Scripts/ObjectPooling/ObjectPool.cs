using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour 
{
    public static ObjectPool Instance;
    public List<GameObject> Pool;
    [SerializeField] private GameObject objectToPool;
    [SerializeField] private int amountToPool;	

    void Awake() 
    { 
        Instance = this;
    }

    private GameObject player;
    
    void Start() 
    {
        for (int i = 0; i < amountToPool; ++i)
        {
            GameObject go = Instantiate(objectToPool, transform);
            go.SetActive(false);
            Pool.Add(go);
            
            SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
            
        }
    }

}