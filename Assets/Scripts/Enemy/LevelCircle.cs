using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCircle : MonoBehaviour
{
    [Header("Effects object")] 
    public LevelCircleEffects easy;
    public LevelCircleEffects medium;
    public LevelCircleEffects hard;
    
    [Header("Objects")] 
    public ParticleSystem PS_InnerCircle;
    public ParticleSystem PS_OuterCircle;
    public ParticleSystem PS_Bling;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(QuestionLevel questionLevel)
    {
        switch (questionLevel)
        {
            case QuestionLevel.Easy:
                SetColor(easy);
                break;
            case QuestionLevel.Medium:
                SetColor(medium);
                break;
            case QuestionLevel.Hard:
                SetColor(hard);
                break;
        }
    }

    public void SetColor(LevelCircleEffects effects)
    {
        var colorOverLifetimeModule = PS_InnerCircle.colorOverLifetime;
        colorOverLifetimeModule.color = effects.inner.colorOverLifetime.color;
        var overLifetimeModule = PS_OuterCircle.colorOverLifetime;
        overLifetimeModule.color = effects.outer.colorOverLifetime.color;
        var colorOverLifetime = PS_Bling.colorOverLifetime;
        colorOverLifetime.color = effects.bling.colorOverLifetime.color;
    }
}
