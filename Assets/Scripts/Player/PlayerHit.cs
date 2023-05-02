using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public Action<BaseEnemy> OnHitEnemy = delegate { };

    private void Start()
    {
        OnHitEnemy += UIGameManager._ins.OpenQuestion;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Enemy"))
        {
            BaseEnemy enemy = collision.gameObject.GetComponent<BaseEnemy>();
            enemy.TakeQuestion();
            OnHitEnemy?.Invoke(enemy);
        }

    }
}
