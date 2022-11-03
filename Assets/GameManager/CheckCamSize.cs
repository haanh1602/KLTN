using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCamSize : MonoBehaviour
{
    public static CheckCamSize _ins;

    

    [SerializeField] Camera cam;
    public Vector3 topRight;
    public Vector3 bottomLeft;
    // Update is called once per frame

    private void Awake()
    {
        _ins = this;
    }
    void Update()
    {
        topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
    }
}
