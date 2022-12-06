using System;
using UnityEngine;
using UnityEngine.UI;

public class UIAvatarItem : MonoBehaviour
{
    public AvatarItem data;
    public Toggle toggle;
    
    //public bool IsOn => toggle.isOn;
}

[Serializable]
public class AvatarItem
{
    public int ID;
    public string Description;
}
