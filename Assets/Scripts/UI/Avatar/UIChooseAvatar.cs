using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIChooseAvatar : MonoBehaviour
{
    [Header("==== Current Avatar ====")]
    [SerializeField] private Image currentAvatar;
    [SerializeField] private Image fillCurrentAvatar;
    [SerializeField] private UIAvatarItem currentAvatarItem;

    [Header("==== Avatars ====")] 
    [SerializeField] private ToggleGroup toggleGroup;

    private List<UIAvatarItem> avatarItems = new List<UIAvatarItem>();
    private AvatarItem tempAvatarItem;
    private void Awake()
    {
        avatarItems = toggleGroup.GetComponentsInChildren<UIAvatarItem>().ToList();
        foreach (var avatarItem in avatarItems)
        {
            avatarItem.toggle.group = toggleGroup;
            if (avatarItem.data.ID == currentAvatarItem.data.ID)
                avatarItem.toggle.isOn = true;
        }

        tempAvatarItem = currentAvatarItem.data;
    }
}
