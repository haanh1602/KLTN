using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoInactive : MonoBehaviour
{
    public void Inactive()
    {
        gameObject.SetActive(false);
    }

    public void Active()
    {
        gameObject.SetActive(true);
    }
}
