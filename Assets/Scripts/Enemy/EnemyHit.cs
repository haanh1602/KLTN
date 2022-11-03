using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public BaseEnemy[] enemies;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("PlayerBullet"))
        {
            
            if (!collision.gameObject.activeInHierarchy)
                return;
            BaseBulletPlayer baseBullet = collision.GetComponent<BaseBulletPlayer>();
            if(!baseBullet.isSpreadDmg)
                baseBullet.DestroyBullet();
            
            for(int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].currentHp > 0)
                {
                    enemies[i].currentHp -= baseBullet.power;
                    if (enemies[i].currentHp <= 0)
                    {
                        enemies[i].Die();
                    }
                }
            }
        }
    }

}
