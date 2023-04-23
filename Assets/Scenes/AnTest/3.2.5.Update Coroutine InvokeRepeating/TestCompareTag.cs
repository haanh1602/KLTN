using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCompareTag : MonoBehaviour
{
    public int amount = 100;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) UseCompareTag();
        if (Input.GetKeyDown(KeyCode.Alpha2)) UseGetTag();
    }

    void UseCompareTag()
    {
        for (int i = 0; i < amount; i++) gameObject.CompareTag("Player");
    }

    void UseGetTag()
    {
        for (int i = 0; i < amount; i++) gameObject.tag.Equals("Player");
    }
}
