using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    public long maxHp;
    public long currentHp;
    public static BasePlayer _ins;
    public PlayerAttack playerAttack;

    public float maxExp;
    public float currentExp;
    public int level = 1;

    public void Awake()
    {
        _ins = this;
    }

    public void LevelUp()
    {
        level++;
        long bonusHp = (long)(maxHp * .1f);

        maxHp += bonusHp;
        currentHp += bonusHp;
        currentExp -= maxExp;
        maxExp *= 1.5f;
        //Select Perk;
    }
    public void Die()
    {
        UImanager._ins.GameOver();

    }



}
