using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipGun : MonoBehaviour
{
    public float Angle;
    private void Update()
    {
        Angle = Vector2.SignedAngle(Vector3.up, -transform.right);
        if (Angle > 0)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
    }
}
