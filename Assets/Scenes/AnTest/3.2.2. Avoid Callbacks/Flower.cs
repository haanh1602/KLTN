using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace An.Optimization
{
    public class Flower : MonoBehaviour
    {
        public float MaxLightRange = 4f;
        public float DefaultLightRange = 0.5f;

        private Light2D light;
        
        private void Awake()
        {
            light = GetComponent<Light2D>();
        }

        /*// Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }*/

        private Coroutine lightCoroutine;

        IEnumerator TurnOnTheLight()
        {
            /*while (GetComponent<Light2D>().pointLightOuterRadius < MaxLightRange)
            {
                GetComponent<Light2D>().pointLightOuterRadius += Time.deltaTime * (MaxLightRange - DefaultLightRange) * 2f;
                yield return null;
            }*/

            while (light.pointLightOuterRadius < MaxLightRange)
            {
                float newValue = Mathf.Min(MaxLightRange, 
                    light.pointLightOuterRadius + Time.deltaTime * (MaxLightRange - DefaultLightRange) * 2f);
                light.pointLightOuterRadius = newValue;
                yield return null;
            }
        }

        IEnumerator TurnOffTheLight()
        {
            /*while (GetComponent<Light2D>().pointLightOuterRadius > DefaultLightRange)
            {
                GetComponent<Light2D>().pointLightOuterRadius -= Time.deltaTime * (MaxLightRange - DefaultLightRange) * 3f;
                yield return null;
            }*/
            while (light.pointLightOuterRadius > DefaultLightRange)
            {
                float newValue = Mathf.Max( DefaultLightRange, 
                    light.pointLightOuterRadius - Time.deltaTime * (MaxLightRange - DefaultLightRange) * 3f);
                light.pointLightOuterRadius = newValue;
                yield return null;
            }
        }
        
        /*private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag.Equals("Player") || other.tag.Equals("Enemy"))
            {
                if (lightCoroutine != null) StopCoroutine(lightCoroutine);
                lightCoroutine = StartCoroutine(TurnOnTheLight());
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag.Equals("Player") || other.tag.Equals("Enemy"))
            {
                if (lightCoroutine != null) StopCoroutine(lightCoroutine);
                lightCoroutine = StartCoroutine(TurnOffTheLight());
            }
        }*/
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Enemy"))
            {
                if (lightCoroutine != null) StopCoroutine(lightCoroutine);
                lightCoroutine = StartCoroutine(TurnOnTheLight());
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Enemy"))
            {
                if (lightCoroutine != null) StopCoroutine(lightCoroutine);
                lightCoroutine = StartCoroutine(TurnOffTheLight());
            }
        }
    }
}

