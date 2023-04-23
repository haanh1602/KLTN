using System.Collections.Generic;
using UnityEngine;

public class MapGenUtility : MonoBehaviour
{
    [SerializeField] private UILevelMapManager mapManager;

    public const string ExportFileAddress = "";
    public string fileName = "";
    
    public UILevelMapManager MapManager
    {
        get
        {
            if (mapManager != null)
            {
                mapManager = FindObjectOfType<UILevelMapManager>();
            }
            return mapManager;
        }
    }

    /*public string GenJsonData()
    {
        
    }*/

    public void WriteJsonDataToFile()
    {
        // Write data here
    }
}
