using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] LineRenderer line;
    
    [SerializeField] Transform endPos;
    public LayerMask layerMask;
    public Transform objTarget;
    void Update()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position,transform.up, layerMask);
        if (raycastHit2D.transform != null)
        {
            endPos.position = raycastHit2D.point;
            objTarget = raycastHit2D.transform;
        }
        else
        {
            endPos.position = transform.position+transform.up*10f;
        }
        Vector3[] vector2s = new Vector3[2];
        vector2s[0] = transform.position;
        vector2s[1] = endPos.position;
        line.SetPositions(vector2s);
    }
}
