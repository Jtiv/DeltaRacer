using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMoveComponent : MonoBehaviour
{
    //Expose these in Editor
    [Header ("Hover Settings")]
    [SerializeField] private List<MagLev> Mags;
    [SerializeField] private GameObject prop;
    [SerializeField] private GameObject cntrMass;
    [SerializeField] private float propulsionMod = 100f;
    [SerializeField] private float torqueMod = 75f;
    [SerializeField] private float dragMod = 5f;

    private Rigidbody rb;

    public void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = cntrMass.transform.localPosition;
    }


    public void HoverMovement(float AxisH, float AxisV)
    {

        foreach (MagLev Mag in Mags)
        {
            Mag.EmitForce();
        }

        if (AxisH != 0 || AxisV != 0)
        {
            //add force according to input and propulsion originating from the propulsion object
            rb.AddForceAtPosition(transform.TransformDirection(Vector3.forward) * AxisV * propulsionMod * Time.fixedDeltaTime, prop.transform.position);
            rb.AddTorque(transform.TransformDirection(Vector3.up) * AxisH * torqueMod * Time.fixedDeltaTime);
        }
        else
        {
            //slow down input is zero on both axes
            rb.velocity *= .95f;
        }

        //stabalizing drag force
        rb.AddForce(Time.fixedDeltaTime * -transform.TransformVector(Vector3.right) * transform.InverseTransformVector(rb.velocity).x * dragMod);


    }

    public void StarshipMovement(float AxisH, float AxisV, Vector2 mouseValues)
    {

        if (AxisH != 0 || AxisV != 0)
        {
            rb.AddForce(transform.TransformDirection(Vector3.forward) * AxisV * (propulsionMod * 2) * Time.fixedDeltaTime);
            rb.AddTorque(transform.TransformDirection(Vector3.forward) * AxisH * -(torqueMod/10) * Time.fixedDeltaTime, ForceMode.Impulse);
            rb.AddForce(transform.TransformDirection(Vector3.right) * AxisH * (torqueMod/10) * Time.fixedDeltaTime, ForceMode.Impulse);
        }
        else
        {
            rb.velocity *= .99f;
        }

        if (mouseValues.magnitude >= .6 || mouseValues.magnitude <= -.6)
        {
            rb.AddTorque(transform.TransformDirection(Vector3.up) * mouseValues.x * (torqueMod / 2) * Time.fixedDeltaTime);
            rb.AddTorque(transform.TransformDirection(Vector3.right) * -mouseValues.y * (torqueMod / 2) * Time.fixedDeltaTime);
        }
       


    }
}
