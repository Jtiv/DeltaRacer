using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Floatable : MonoBehaviour, IOrbit
{
    [SerializeField]
    private float planetGravResponseMod = 1f;
    [SerializeField]
    private float FloatingHeight = 3f;
    private bool _inGravField = false;
    private float _planetGravMod;
    private Vector3 _planetCenterMass;

    protected Rigidbody rb;


    [SerializeField] private float degreesPerSecond = 15.0f;
   
    // Update is called once per frame
    void FixedUpdate()
    {
        if (_inGravField == true)
        { 
            Vector3 subjGravityDirection = (_planetCenterMass - rb.position);
            Debug.DrawRay(rb.position, subjGravityDirection, Color.red); // <--- delete later
            float singleStep = planetGravResponseMod * Time.fixedDeltaTime;
            Vector3 reAngle = Vector3.RotateTowards(rb.transform.forward, -subjGravityDirection, singleStep, 0.0f);
            rb.transform.rotation = Quaternion.RotateTowards(rb.transform.rotation, Quaternion.Euler(reAngle), singleStep);

            RaycastHit hitinfo;
            if (Physics.Raycast(rb.position, subjGravityDirection, out hitinfo, 20f, 1 << 8))
            {
                rb.transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.Self);

                if (hitinfo.distance > FloatingHeight)
                {
                    rb.AddForce(subjGravityDirection.normalized * (_planetGravMod/2) * Time.fixedDeltaTime);
                }

                else
                {
                    rb.AddForce(-subjGravityDirection.normalized * (_planetGravMod/2) * Time.fixedDeltaTime);
                }
            }
        }
    }
    
    public void SetGravDir(Vector3 centerMass, float gravModifier)
    {
        _planetGravMod = gravModifier;
        _planetCenterMass = centerMass;
        _inGravField = true;
    }

    public void ResetGravDir()
    {
        _inGravField = false;
    }
}
