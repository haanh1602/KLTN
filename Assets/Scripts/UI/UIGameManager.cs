using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameManager : MonoBehaviour
{
    public static UIGameManager _ins;

    public Joystick joystick;
    public GameObject question;

    private void Awake()
    {
        _ins = this;
    }

    private void Start()
    {
        OpenJoystick();
    }

    public void OpenQuestion(BaseEnemy enemy)
    {
        Time.timeScale = 0;
        UIManager.Instance.questionController.Init(enemy);
        joystick.enabled = false;
        question.active = true;
    }

    public void OpenJoystick()
    {
        Time.timeScale = 1;
        question.active = false;
        joystick.enabled = true;
    }
}
