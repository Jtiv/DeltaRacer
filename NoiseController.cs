using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseController
{
   
    public float strength = 1f;
    public float roughness = 1f;

    Noise noise = new Noise();

    public float SampleAtPosition(Vector3 position)
    {
        float value = (noise.Evaluate(position * roughness) + 1) / 2f;

        //do processing

        return value * strength;
    }

    public float SampleAtRandPosition(Vector3 position, Vector3 offset)
    {
        

        float value = (noise.Evaluate(position * roughness + offset) + 1) / 2f;

        //do processing

        return value * strength;
    }
    
}
