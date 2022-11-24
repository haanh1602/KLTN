using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    public Vector3 sizeRender = new Vector3(50f, 50f, 0);

    private void Start()
    {
        sizeRender *= 2;
    }

    private void Update()
    {
        var cameraPosition = Camera.main.transform.position;
        var dirX = 0;
        var dirY = 0;
        
        if (cameraPosition.x - transform.position.x < -sizeRender.x)
        {
            dirX = -1;
        }
        else if (cameraPosition.x - transform.position.x > sizeRender.x)
        {
            dirX = 1;
        }
        
        if (cameraPosition.y - transform.position.y < -sizeRender.y)
        {
            dirY = -1;
        }
        else if (cameraPosition.y - transform.position.y > sizeRender.y)
        {
            dirY = 1;
        }
        
        transform.position += new Vector3(2 * sizeRender.x * dirX, 2 * sizeRender.y * dirY, 0);
    }
}
