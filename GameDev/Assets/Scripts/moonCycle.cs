﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moonCycle : MonoBehaviour
{
    new private Light light;
    
    void Start() {
        light = GetComponent<Light>();
    }

    
    void Update() {
        transform.RotateAround(Vector3.zero, Vector3.right, .5f * Time.deltaTime);
        transform.LookAt(Vector3.zero);
        // Trees look weird when sun or moon is rising, especially the moon, trying to negate the effect.
        if (transform.position.y < 0 && light.intensity > 0) {
            light.intensity -= .001f;
        }
        else if (transform.position.y > 0 && light.intensity < .5f) {
            light.intensity += .001f;
        }

    }
}
