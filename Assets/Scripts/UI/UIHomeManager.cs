using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHomeManager : MonoBehaviour
{
    public void OnClickButtonHome()
    {
        AudioManager.Instance.PlayOnClick();
        SceneController.Instance.LoadScene("Menu");
    }
}
