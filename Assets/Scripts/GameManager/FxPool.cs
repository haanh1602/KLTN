using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxPool : MonoBehaviour
{
    public int idPool;

    public float duration;

    public ParticleSystem fx;

    private void OnEnable()
    {
        StartCoroutine(AddToPool());
    }

    IEnumerator AddToPool()
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
        PoolFxManager._ins.AddToPool(this);
    }
}
