using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    private bool isLoading = false;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    IEnumerator AsyncLoadScene(string sceneName)
    {
        if (isLoading) yield break;
        isLoading = true;
        NotifyUI.Instance.ShowLoading();
        yield return new WaitForSeconds(0.25f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        GameData.Instance.InitQuestionData();
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        NotifyUI.Instance.DisableLoading();
        isLoading = false;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(AsyncLoadScene(sceneName));
    }
}
