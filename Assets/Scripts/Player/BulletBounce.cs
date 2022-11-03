using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBounce : MonoBehaviour
{
    
    bool isBounce = true;


    private void OnEnable()
    {
        isBounce = true;
    }

    private void Update()
    {
        if (!isBounce) return;
        if(transform.position.x<CheckCamSize._ins.bottomLeft.x|| transform.position.x > CheckCamSize._ins.topRight.x)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0,  - transform.rotation.eulerAngles.z));
            isBounce = false;
            if (transform.position.x < CheckCamSize._ins.bottomLeft.x)
            {
                transform.position = new Vector3(CheckCamSize._ins.bottomLeft.x, transform.position.y);
            }
            else
            {
                transform.position = new Vector3(CheckCamSize._ins.topRight.x, transform.position.y);
            }
        }
    }
}
