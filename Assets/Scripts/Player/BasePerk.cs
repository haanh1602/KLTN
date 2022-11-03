using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePerk : MonoBehaviour
{
    public int level;
    public int maxLevel;
    public float timeCD;

    public virtual void Attack()
    {
        if (level <= 0)
        {
            return;
        }
    }

    public virtual void UpgradePerk()
    {
        Debug.Log("Upgrade " + name);
        level++;
    }
}
