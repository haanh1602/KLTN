using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameManager : MonoBehaviour
{
    public static UIGameManager _ins;

    public Joystick joystick;


    private void Awake()
    {
        _ins = this;
    }
}
