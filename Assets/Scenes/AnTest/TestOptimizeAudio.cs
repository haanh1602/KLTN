using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOptimizeAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool test;
    // Update is called once per frame
    void Update()
    {
        if (test)
        {
            test = false;
            audioSource.Play();
        }
    }
}
