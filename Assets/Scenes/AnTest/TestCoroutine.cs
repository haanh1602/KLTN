using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCoroutine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IETest());
    }

    IEnumerator IETest()
    {
        while (true)
        {
            Debug.Log("Coroutine đang chạy!");
            yield return new WaitForSeconds(0.5f);
        }
    }
}
