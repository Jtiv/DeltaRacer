using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIDControllerTest : MonoBehaviour
{

    // The gains are chosen experimentally
    public float Kp = 1;
    public float Ki = 0;
    public float Kd = 0.1f;

    private float prevError;
    private float P, I, D;

    public float GetOutput(float currentError)
    {
        P = currentError;
        I += P * Time.deltaTime;
        D = (P - prevError) / Time.deltaTime;
        prevError = currentError;

        return P * Kp + I * Ki + D * Kd;
    }


}
