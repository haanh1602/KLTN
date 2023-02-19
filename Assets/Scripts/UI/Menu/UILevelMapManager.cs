using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelMapManager : MonoBehaviour
{
    [SerializeField] private ToggleGroup toggleGroup;
    [SerializeField] private List<UILevelButton> listLevelButton = new List<UILevelButton>();

    private void Awake()
    {
        toggleGroup.allowSwitchOff = true;
    }

    private void OnEnable()
    {
        for (int i = 0; i < listLevelButton.Count; i++)
        {
            int temp = i + 1;
            listLevelButton[i].Init(temp);
            listLevelButton[i].SetToggleGroup(toggleGroup);
        }
    }
}
