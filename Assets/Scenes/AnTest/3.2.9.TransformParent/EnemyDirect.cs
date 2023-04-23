using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace An.Optimization
{
    public class EnemyDirect : MonoBehaviour
    {
        public float displayDistance = 4f;
        public SpriteRenderer spriteRenderer;
        public BasePlayer player;
        public Vector2 firstFaceDirect = Vector2.down;

        // Update is called once per frame
        void LateUpdate()
        {
            BaseEnemy nearestEnemy = global::EnemyManager._ins.GetNearestEnemyList(player.transform.position, out var sqrDistance);
            spriteRenderer.enabled = sqrDistance > displayDistance;
            if (nearestEnemy == null) return;
            Vector2 cageDirect = nearestEnemy.transform.position - transform.position;
            transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(firstFaceDirect, cageDirect));
        }
    }
}

