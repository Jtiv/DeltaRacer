using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagLev : MonoBehaviour
{
    [SerializeField] private float magForceMod = 250f;
    [SerializeField] private float magLengthMod = 3f;

    private Rigidbody parentRB;

    public void OnEnable()
    {
        parentRB = GetComponentInParent<Rigidbody>();
    }

    public void EmitForce()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.down), out hitInfo, magLengthMod))
        {
           parentRB.AddForceAtPosition(Time.fixedDeltaTime * transform.TransformDirection(Vector3.up)     
           * 
           Mathf.Pow(magLengthMod - hitInfo.distance, 2) / magLengthMod * magForceMod, transform.position);
        }
    }

    
}
