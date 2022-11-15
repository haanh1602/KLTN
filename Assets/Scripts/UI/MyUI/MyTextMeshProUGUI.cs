using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class MyTextMeshProUGUI : TextMeshProUGUI
{
    public override string text
    {
        get => m_text;
        set
        {
            var change = !m_text.Equals(value);
            base.text = value;
            if (change)
            {
                OnChangeText?.Invoke();
            }
        }
    }

    public Action OnChangeText = delegate { }; 

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        OnChangeText += () =>
        {
            Debug.LogError(text);
        };
        //StartCoroutine(Test());
    }

    public IEnumerator Test()
    {
        yield return new WaitForSeconds(1f);
        text += "n";
        StartCoroutine(Test());
    }
}
