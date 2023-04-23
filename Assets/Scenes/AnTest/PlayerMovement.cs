using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace An.Optimization
{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed = 2.5f;
        
        private int x = 0, y = 0;
        // Update is called once per frame
        void Update()
        {
            x = y = 0;
            if (Input.GetKeyDown(KeyCode.LeftArrow)) x--;
            if (Input.GetKeyDown(KeyCode.RightArrow)) x++;
            if (Input.GetKeyDown(KeyCode.UpArrow)) y++;
            if (Input.GetKeyDown(KeyCode.DownArrow)) y--;
            if (x != 0 && y != 0)
            {
                transform.position += new Vector3(x, y).normalized * speed;
            }
        }
    }

}
