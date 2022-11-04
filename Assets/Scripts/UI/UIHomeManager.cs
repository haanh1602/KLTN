using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHomeManager : MonoBehaviour
{
    public void OnClickButtonHome()
    {
        MapHandle.seed = new Vector2(Random.Range(10f, 12f), Random.Range(70f, 82f));
        StartCoroutine(AsyncLoadScene("Level"));
    }

    private bool isLoading = false;
    
    IEnumerator AsyncLoadScene(string sceneName)
    {
        if (isLoading) yield break;
        isLoading = true;
        //NotifyUI.Instance.ShowLoading();
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
}
