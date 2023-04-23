using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoInactive : MonoBehaviour
{
    public void Inactive()
    {
        gameObject.SetActive(true);
    }

    public void Active()
    {
        gameObject.SetActive(false);
    }
}
