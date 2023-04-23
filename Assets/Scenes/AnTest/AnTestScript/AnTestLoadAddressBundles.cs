using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.UI;
using Object = System.Object;

public class AnTestLoadAddressBundles : MonoBehaviour
{
    public AssetReference asset;
    public TextMeshProUGUI tmpText;

    public Button btnReload;
    public TextMeshProUGUI tmpButtonReloadText;

    public Slider slider;
    
    public string downloadKey = "default";
    
    private bool loadingNewText = false;

    private bool downloadedBundle = false;
    
    private void Awake()
    {
        btnReload.onClick.AddListener(DownLoadAssetBundle);
        //tmpButtonReloadText.text = "RELOAD";
        StartCoroutine(GetAssetBundle());
    }
    
    IEnumerator GetAssetBundle()
    {
        AsyncOperationHandle<long> getDownloadSizeHandle = Addressables.GetDownloadSizeAsync(downloadKey);
        yield return getDownloadSizeHandle;
        if (getDownloadSizeHandle.Result > 0)
        {
            tmpButtonReloadText.text = "DOWNLOAD " +
                                       (getDownloadSizeHandle.Result / 1024f).ToString("F1",
                                           CultureInfo.InvariantCulture) + "KB";
            Debug.LogError("Da den duoc day : " + (getDownloadSizeHandle.Result / 1024f).ToString("F1",
                CultureInfo.InvariantCulture) + "KB");
            btnReload.onClick.RemoveAllListeners();
            btnReload.onClick.AddListener(DownLoadAssetBundle);
        }
        else
        {
            tmpButtonReloadText.text = "START";
            downloadedBundle = true;
            btnReload.onClick.RemoveAllListeners();
            btnReload.onClick.AddListener(() => StartCoroutine(IELoadAsset()));
        }

        /*UnityWebRequest www =
            UnityWebRequestAssetBundle.GetAssetBundle(
                "https://drive.google.com/drive/u/0/folders/13yPQyKaTgj4NYiiBv5GGY4yMCQgWbsPS");
        yield return www.SendWebRequest();
 
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
            yield return new WaitForSeconds(1f);
            StartCoroutine(GetAssetBundle());
        }
        else {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            downloadedBundle = true;
        }*/
    }
    
    IEnumerator IEChangeDownloadProgressBar(AsyncOperationHandle preDownloadHandle)
    {
        float percentComplete = preDownloadHandle.GetDownloadStatus().Percent;
        slider.value = percentComplete * 0.9f;
        //Debug.LogError(preDownloadHandle.PercentComplete + " " + preDownloadHandle.GetDownloadStatus().Percent);
        if (percentComplete >= 1f)
            yield break;

        yield return null;
        StartCoroutine(IEChangeDownloadProgressBar(preDownloadHandle));
    }

    public void DownLoadAssetBundle()
    {
        btnReload.onClick.RemoveAllListeners();
        StartCoroutine(IEDownloadAssetBundle());
    }

    IEnumerator IEDownloadAssetBundle()
    {
        AsyncOperationHandle downloadDependencies = Addressables.DownloadDependenciesAsync(downloadKey);
        StartCoroutine(IEChangeDownloadProgressBar(downloadDependencies));
        yield return downloadDependencies;

        // Xoa cache
        //Addressables.CleanBundleCache();
        ClearOldData();
        downloadedBundle = true;
        tmpButtonReloadText.text = "START";
        btnReload.onClick.RemoveAllListeners();
        btnReload.onClick.AddListener(() => StartCoroutine(IELoadAsset()));
        //StartCoroutine(LoadResourcesIntoCustomSkinController());
    }
    IEnumerator IELoadAsset()
    {
        loadingNewText = true;
        tmpButtonReloadText.text = "LOADING...";
        yield return new WaitUntil(() => downloadedBundle);
        //if (asset.Asset == null) yield return new WaitUntil(() => asset.LoadAssetAsync<TextAsset>());
        Debug.LogError("Da den duoc load");
        if (asset.Asset == null)
        {
            AsyncOperationHandle<TextAsset> a = asset.LoadAssetAsync<TextAsset>();
            yield return a;
            a.Completed += (handle) =>
            {
                StartCoroutine(IEDisplayAsset((a.Result.text)));
            };
        }
        else
        {
            yield return StartCoroutine(IEDisplayAsset(((TextAsset) asset.OperationHandle.Result).text));
        }

        tmpButtonReloadText.text = "RELOAD";
        loadingNewText = false;
    }

    IEnumerator IEDisplayAsset(string text)
    {
        tmpText.text = "";
        for (int i = 0; i < text.Length; i++)
        {
            tmpText.text += text[i];
            yield return null;
        }
    }
    
    void ClearOldData()
    {
        var bundlesHash = new HashSet<string>();
        foreach (var loc in Addressables.ResourceLocators)
        {
            foreach (var key in loc.Keys)
            {
                if (!loc.Locate(key, typeof(object), out var resourceLocations))
                    continue;

                foreach (var d in resourceLocations.Where(l => l.HasDependencies).SelectMany(l => l.Dependencies))
                {
                    if (d.Data is AssetBundleRequestOptions dependencyBundle)
                    {
                        bundlesHash.Add(dependencyBundle.Hash);
                    }
                }
            }
        }

        var path = Caching.GetCacheAt(0).path;
        if (!Directory.Exists(path))
            return;

        foreach (var directory in Directory.GetDirectories(path).SelectMany(Directory.GetDirectories))
        {
            if (!bundlesHash.Contains(new DirectoryInfo(directory).Name))
            {
                Directory.Delete(directory, true);
            }
            else
            {
                Debug.LogError(new DirectoryInfo(directory).Name);
            }
        }
    }
}
