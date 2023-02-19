using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    public static BasePlayer _ins;

    public float exp;
    public int level = 1;

    public void Awake()
    {
        _ins = this;
    }

    public void LevelUp()
    {
        level++;
    }
    public void Die()
    {
        UImanager._ins.GameOver();
    }



}
