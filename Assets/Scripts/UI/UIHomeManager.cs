using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHomeManager : MonoBehaviour
{
    public void buttonHome()
    {
        MapHandle.seed = new Vector2(Random.Range(10f, 12f), Random.Range(70f, 82f));
        SceneManager.LoadScene("ScenesLevel");
    }
}
