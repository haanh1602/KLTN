using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InGameManager : MonoBehaviour
{
    public static InGameManager _ins;

    public MapManager mapManager;


    public void Awake()
    {
        _ins = this;

    }

    private void OnEnable()
    {
        mapManager.GenerateMap(new Vector2(0, 0));
    }


}
