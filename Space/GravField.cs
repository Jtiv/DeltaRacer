using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravField : MonoBehaviour
{
    //properties of planet
    //scale gravity field mesh to match planet mesh with 60 extra units of scale

    [SerializeField]
    private float _gravModifier;

    private Vector3 _corePos;
  
    public void SetCenterOfMass(Transform planetTransform)
    {
        _corePos = planetTransform.position;
        //Make custom gravity force, but for now using gravity. Just want absolute value so (grav)*-1
        //_gravModifier = 90f;
    }

    public Vector3 CenterofMass()
    {
        return _corePos;
    }

    private void OnTriggerEnter(Collider Satellite)
    {
        //supply objects with grav force and direction
        IOrbit satellite = Satellite.GetComponent<IOrbit>();
        if (satellite != null)
        {
            satellite.SetGravDir(_corePos, _gravModifier);
            //Debug.Log($"Planet Says {Satellite.name} is here");
        }else
        {
            //Debug.Log($" ENTER: {Satellite.name} does not inherit from satellite class");
        }
    }

    private void OnTriggerExit(Collider Satellite)
    {
        IOrbit satellite = Satellite.GetComponent<IOrbit>();
        if (satellite != null)
        {
            satellite.ExitGrav();
            //Debug.Log($"Planet Says {Satellite.name} is exiting");
        }
        if (Satellite.gameObject.tag != "Player")
        {
            Destroy(Satellite.gameObject);
        }
        
    }

}
