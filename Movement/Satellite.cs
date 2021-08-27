using UnityEngine;

public abstract class Satellite : MonoBehaviour, IOrbit
{
    [SerializeField]
    private float planetGravResponseMod = 1f;
    protected bool _inGravField = false;
    private float _planetGravMod;
    private Vector3 _planetCenterMass;

    protected Rigidbody rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    protected virtual void FixedUpdate()
    {

        if (_inGravField == true)
        {
            Vector3 subjGravityDirection = (_planetCenterMass - rb.position);
            Vector3 subjGravForce = subjGravityDirection.normalized * _planetGravMod;
            rb.AddForce(subjGravForce * Time.fixedDeltaTime);
            Debug.DrawRay(rb.position, subjGravityDirection, Color.red);
            float singleStep = planetGravResponseMod * Time.fixedDeltaTime;
            Vector3 reAngle = Vector3.RotateTowards(rb.transform.forward, -subjGravityDirection, singleStep, 0.0f);
            rb.transform.rotation = Quaternion.RotateTowards(rb.transform.rotation, Quaternion.Euler(reAngle), singleStep); 
        }
    }

    
    public void SetGravDir(Vector3 centerMass, float gravModifier)
    {
        _planetGravMod = gravModifier;
        _planetCenterMass = centerMass;
        _inGravField = true;
    }

    public void ExitGrav()
    {
        _inGravField = false;
    }

    public Vector3 GetPlanetCenterMass()
    {
        return _planetCenterMass;
    }

    public bool HasGravDir()
    {
        return _inGravField;
    }
}
