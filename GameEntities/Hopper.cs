using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hopper : Floatable
{
    [SerializeField]
    private GameObject anothaOne; 
    private Transform Target;

    private float axisH = 0f, axisV = 0f;

    [SerializeField]
    private GameObject TriggerObject;

    private EasyMover EasyMover;
    
    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        EasyMover = GetComponent<EasyMover>();
        

        SphereCollider sc = TriggerObject.AddComponent<SphereCollider>();
        sc.isTrigger = true;
    }

   
    // Update is called once per frame
   

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if(Target = null)
        {
            rb.angularVelocity *= .75f;
            rb.velocity *= .65f;
        }
        else
        {
            EasyMover.MoveToward(Target);
        }
        
        
    }

    private void OnCollisionEnter(Collision PlayerHopefully)
    {
        if (PlayerHopefully.gameObject.tag == "Player")
        {
            PlayerHopefully.gameObject.GetComponent<PlayerRacer>().Health -= 10f;
            PlayerHopefully.rigidbody.AddForce(transform.forward * 25f, ForceMode.Impulse);
            Instantiate(anothaOne);
        }
        
    }
    private void OnTriggerEnter(Collider PlayerHopeFully)
    {
        if (PlayerHopeFully.gameObject.tag == "Player")
        {
            Target = PlayerHopeFully.gameObject.transform;
        }
    }

    private void OnTriggerStay(Collider PlayerHopefully)
    {
        if (PlayerHopefully.gameObject.tag == "Player")
        {
            EasyMover.MoveToward(PlayerHopefully.gameObject.transform);
        }
        
    }

    private void OnTriggerExit(Collider PlayerHopefully)
    {
        if (PlayerHopefully.gameObject.tag == "Player")
        {
            Target = null;
        }
    }

}
