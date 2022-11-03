using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    public static PlayerMovement _ins;
    [SerializeField] Animator anim;
    private void Start()
    {
        BasePlayer._ins.transform.position = new Vector3(0, -3, 0);
    }
    void Update()
    {
        
        //anim.SetFloat("Speed", UIGameManager._ins.joystick.Direction.magnitude);
        
    }
}
