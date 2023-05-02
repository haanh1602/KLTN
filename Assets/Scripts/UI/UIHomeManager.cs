using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHomeManager : MonoBehaviour
{
    public void OnClickButtonHome()
    {
        SceneController.Instance.LoadScene("Menu");
    }
}
