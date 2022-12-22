using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILevelButton : MonoBehaviour
{ 
    private int level = 1;
    [SerializeField] private Image iconImage;
    [SerializeField] private Toggle toggle;
    [SerializeField] private TextMeshProUGUI tmpLevel;
    [SerializeField] private Button btnStart;
    [SerializeField] private DOTweenAnimation dotAnimation;

    public int Level => level;

    private void Awake()
    {
        toggle.onValueChanged.RemoveAllListeners();
        toggle.onValueChanged.AddListener(OnToggleCLick);
    }

    private void Start()
    {
        btnStart.onClick.AddListener(() =>
        {
            FindObjectOfType<UIMenuManager>().OnStartGame(level);
            gameObject.SetActive(false);
        });
    }

    public void OnToggleCLick(bool isOn)
    {
        dotAnimation.DORestartById(isOn ? "levelInfo_open" : "levelInfo_close");
    }

    public void Init(int level)
    {
        this.level = level;
        tmpLevel.text = level.ToString();
        toggle.interactable = level <= GameData.Instance.LevelUnlock;
    }

    public void SetToggleGroup(ToggleGroup toggleGroup)
    {
        toggle.group = toggleGroup;
    }
}
