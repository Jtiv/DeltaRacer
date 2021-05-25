using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Leaper : Satellite
{

    private float axisH = 0f, axisV = 0f;

    [SerializeField]
    private GameObject TriggerObject;
    
    private ShipMoveComponent ShipMoveComponent;

    [SerializeField]
    private float Tspeed = -30f;

    private void Update()
    {
        Debug.DrawRay(rb.transform.position, rb.transform.TransformDirection(Vector3.forward), Color.green, 5f);

    }

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        ShipMoveComponent = GetComponent<ShipMoveComponent>();
        

        SphereCollider sc = TriggerObject.AddComponent<SphereCollider>();
        sc.isTrigger = true;
    }

   
    // Update is called once per frame
   

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        ShipMoveComponent.HoverMovement(axisH, axisV);

        if(axisH == 0 || axisV == 0)
        {
            rb.angularVelocity *= .55f;
            
        }
        
        
    }

    private void OnCollisionEnter(Collision PlayerHopefully)
    {
        if (PlayerHopefully.gameObject.tag == "Player")
        {
            PlayerHopefully.gameObject.GetComponent<PlayerRacer>().Health -= 10f;
            PlayerHopefully.rigidbody.AddForce(transform.forward * 100f, ForceMode.Impulse);
        }



    }




    private void OnTriggerStay(Collider PlayerHopefully)
    {
        if (PlayerHopefully.gameObject.tag == "Player")
        {
            //subtract transform from this transform and translate that into directions on two Axes
            Vector3 Direction = PlayerHopefully.gameObject.transform.position - rb.position;
            Vector3 NewFwd = Vector3.RotateTowards(transform.forward, Direction, Tspeed * Time.deltaTime, 0.0f);
            var ReAd = Quaternion.LookRotation(NewFwd, transform.up);
            
            axisH = ReAd.y;
            if (axisH < .5f)
            {
                axisV = ReAd.w;
            }
            
        }
        
    }

    private void OnTriggerExit(Collider PlayerHopefully)
    {
        if (PlayerHopefully.gameObject.tag == "Player")
        {

            axisV = 0;
            axisH = 0;

        }
    }

}
