using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    public static PlayerMovement _ins;

    void Update()
    {
        if (GameManager.Instance.IsAnswering) return;
        
        Vector2 nextPos = UIGameManager._ins.joystick.Direction.normalized * speed * Time.deltaTime;
        BasePlayer._ins.transform.position += (Vector3)nextPos;
    }
}
