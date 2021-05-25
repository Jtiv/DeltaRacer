using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravField : MonoBehaviour
{
    //properties of planet

    [SerializeField]
    private float _gravModifier;

    [SerializeField]
    private Rigidbody _rigidbody;

    private Vector3 _corePos;
    private Collider _collider;

    private void Awake()
    {
        _corePos = _rigidbody.position;

        //Make custom gravity force, but for now using gravity. Just want absolute value so (grav)*-1
        //_gravModifier = 90f;
    }

    /*
    private void OnTriggerStay(Collider Satellite)
    {
        IOrbit satellite = Satellite.GetComponent<IOrbit>();
        if (satellite != null && !satellite.HasGravDir())
        {
            satellite.SetGravDir(_corePos, _gravModifier);
        }
         
    }*/

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
