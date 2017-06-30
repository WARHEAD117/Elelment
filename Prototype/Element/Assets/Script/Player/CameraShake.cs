using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    public enum ShakeType
    {
        Horizontal,
        Vertical,
        Sphere
    }
    public ShakeType shakeType;

    void Awake()
    {
        shakeType = ShakeType.Sphere;
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            if(shakeType == ShakeType.Horizontal)
            {
                camTransform.localPosition = originalPos + new Vector3(Random.Range(-1, 1), 0,0) * shakeAmount;
            }
            else if(shakeType == ShakeType.Vertical)
            {
                camTransform.localPosition = originalPos + new Vector3(0, Random.Range(-1, 1),  0) * shakeAmount;
            }
            else if (shakeType == ShakeType.Sphere)
            {
                camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            }
            else
            {
                camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            }

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }
}

