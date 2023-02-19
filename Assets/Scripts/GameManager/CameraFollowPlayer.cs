using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] Transform cacheCamera;

    public float speed;

    void Update()
    {
        cacheCamera.position=Vector3.Lerp(cacheCamera.position,BasePlayer._ins.transform.position,Time.deltaTime*speed);
    }
}
