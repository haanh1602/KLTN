using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLaser : MonoBehaviour
{
    [SerializeField] LineRenderer laser;
    public float speedScroll;
    
    public Material material;

    float size;
    public float length;

    public Vector2 sizeTex;
    public void Awake()
    {
        material = laser.material;
        size = laser.startWidth;
        sizeTex = new Vector2(material.mainTexture.width, material.mainTexture.height);
    }

    private void Update()
    {
        length = Vector3.Distance((Vector2)laser.GetPosition(0), (Vector2)laser.GetPosition(1));
        material.mainTextureOffset += (new Vector2(1, 0)) * Time.deltaTime* speedScroll;
        material.mainTextureScale = new Vector2((length / size) / (sizeTex.x / sizeTex.y), 1);
        //material.
    }
}
