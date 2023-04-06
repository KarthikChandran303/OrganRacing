using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Source: https://www.youtube.com/watch?v=BQGTdRhGmE4
public class CameraShake : MonoBehaviour
{

    public bool start = false;

    [SerializeField] float shakeDuration = 2f;
    [SerializeField] AnimationCurve intensityCurve;

    private bool shaking;

    Vector3 StartPosition;

    public void StartShaking()
    {
        shaking = true;
        StartCoroutine("Shaking");
    }

    public void StopShaking()
    {
        shaking = false;
    }

    public void update() {
        if (start) {
            start = false;
            StartCoroutine("Shaking");
        }
    }

    IEnumerator Shaking()
    {
        while (shaking){
            //Debug.Log("Reached this.");
            Vector3 StartPosition = transform.localPosition;
            float elapsedTime = 0f;
            while(elapsedTime < shakeDuration)
            {
                StartPosition = transform.position;
                elapsedTime += Time.deltaTime;
                float strength = intensityCurve.Evaluate(elapsedTime / shakeDuration);
                transform.localPosition = StartPosition + Random.insideUnitSphere * strength;
                yield return null;
            }

            transform.localPosition = StartPosition;
        }
    }
}
