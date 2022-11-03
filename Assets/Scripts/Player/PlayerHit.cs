using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Enemy"))
        {
            
            BaseEnemy enemy = collision.gameObject.GetComponent<BaseEnemy>();

            BasePlayer._ins.currentHp -= enemy.damage;
            UImanager._ins.SetUIHp();
            if (BasePlayer._ins.currentHp <= 0)
            {
                BasePlayer._ins.Die();
            }
        }

    }
}
