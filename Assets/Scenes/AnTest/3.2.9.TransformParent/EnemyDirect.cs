using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace An.Optimization
{
    public class EnemyDirect : MonoBehaviour
    {
        public BasePlayer player;
        public Vector2 firstFaceDirect = Vector2.down;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            Enemy nearestEnemy = EnemyManager.Instance.GetNearestEnemyList(player.transform.position);
            if (nearestEnemy == null) return;
            Vector2 cageDirect = nearestEnemy.transform.position - transform.position;
            transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(firstFaceDirect, cageDirect));
        }
    }
}

