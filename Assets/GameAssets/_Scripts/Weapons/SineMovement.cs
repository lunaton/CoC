using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineMovement : MonoBehaviour
{
    [SerializeField] float Period           = 1;
    [SerializeField] float Amplitude        = 1;
    [SerializeField] float Offset           = 0;

    Vector3 _initialPosition;

    private void Start()
    {
        _initialPosition = transform.localPosition;
    }

    void Update()
    {
        float sin = (Mathf.Sin(Time.time * Period) * Amplitude + Offset) * Time.deltaTime;
        Vector3 SinUp = Vector3.up * sin;
        Vector3 frameLocation = _initialPosition + SinUp;

        transform.localPosition = frameLocation;
    }
}
