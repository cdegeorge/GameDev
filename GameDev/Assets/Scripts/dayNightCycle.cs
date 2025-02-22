﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dayNightCycle : MonoBehaviour
{
    new private Light light;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();   
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.right, 10f * Time.deltaTime);
        transform.LookAt(Vector3.zero);
        // Trees look weird when sun or moon is rising, especially the moon, trying to negate the effect.
        if (transform.position.y < -20) {
            light.intensity -= .001f;
        } else if (transform.position.y > -20 && light.intensity < .05f) {
            light.intensity += .001f;
        }

    }
}
