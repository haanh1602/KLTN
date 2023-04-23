using System;
using UnityEngine;

namespace An.Optimization
{
    public class Enemy : MonoBehaviour
    {
        public string id;
        
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Animator animator;
        [SerializeField] private LevelCircle levelCircle;

        public Vector3 firstFaceDirection = Vector3.left;
        private Vector3 direction = Vector3.zero;

        public bool isAlive = true;

        // 4.2.1.3 - Keo tha
        private void Awake()
        {
            if (spriteRenderer == null) 
                spriteRenderer = GetComponent<SpriteRenderer>();
            if (animator == null) 
                animator = GetComponent<Animator>();
            if (levelCircle == null) 
                levelCircle = GetComponent<LevelCircle>();
        }

        // 4.2.1.3 - GetComponent
        /*private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            levelCircle = GetComponent<LevelCircle>();
        }*/
        
        public float speed = 5f;
        
        // 4.2.1.1
        /*private void Awake()
        {
            switch (EnemySpawner.AwakeType)
            {
                case 0:
                    GetComponentString();
                    break;
                case 1:
                    GetComponentTypeOfT();
                    break;
                case 2:
                    GetComponentT();
                    break;
            }
        }*/

        private static BasePlayer _player;
        private static BasePlayer Player
        {
            get
            {
                if (System.Object.ReferenceEquals(_player, null) /*_player == null*/) 
                    _player = FindObjectOfType<BasePlayer>();
                return _player;
            }
        }

        private static Transform PlayerTransform => Player.transform;
        
        private void Update()
        {
            Move();
        }
        
        /*void Move()
        {
            direction = Player.transform.position - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, 
                Player.transform.position, speed * Time.deltaTime);
            bool changeDirection = direction.x * firstFaceDirection.x < 0;
            if (changeDirection)
                transform.localScale = 
                    new Vector3(transform.localScale.x * -1, transform.localScale.y, 
                        transform.localScale.z);
        }
        */

        // 4.2.1.10
        
        void Move()
        {
            var cacheTransform = transform;
            Vector3 position = cacheTransform.position;
            Vector3 playerPosition = Player.transform.position;
            direction = playerPosition - position;
            position = Vector3.MoveTowards(position, 
                playerPosition, speed * Time.deltaTime);
            transform.position = position;
            bool changeDirection = direction.x * firstFaceDirection.x < 0;
            if (changeDirection)
            {
                Vector3 localScale = cacheTransform.localScale;
                localScale = new Vector3(localScale.x * -1, localScale.y, localScale.z);
                cacheTransform.localScale = localScale;
            }
        }

        /*
        void Move()
        {
            BasePlayer player = FindObjectOfType<BasePlayer>();
            transform.position = Vector3.MoveTowards(transform.position, 
                player.transform.position, speed * Time.deltaTime);
        }
        */

        void GetComponentString()
        {
            spriteRenderer = GetComponent("SpriteRenderer") as SpriteRenderer;
            animator = GetComponent("Animator") as Animator;
            levelCircle = GetComponent("LevelCircle") as LevelCircle;
        }

        void GetComponentTypeOfT()
        {
            spriteRenderer = GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
            animator = GetComponent(typeof(Animator)) as Animator;
            levelCircle = GetComponent(typeof(LevelCircle)) as LevelCircle;
        }

        void GetComponentT()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            levelCircle = GetComponent<LevelCircle>();
        }
        
        
        
    }
}

