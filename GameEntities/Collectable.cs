using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Floatable
{
    float timer;
    Collider Collider;
    bool _colliderEnabled = false;
    
    void Awake()
    {
        timer = 0f;
        rb = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
        rb.AddForce(Vector3.up * Random.Range(.05f, .15f), ForceMode.Impulse);
        rb.AddForce(Vector3.forward * Random.Range(.01f, .05f), ForceMode.Impulse);
        Collider.isTrigger = false;
        
    }



    private void Update()
    {
        if (_colliderEnabled == false && timer > 2f)
        {
            Collider.isTrigger = true;
            _colliderEnabled = true;
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        timer += Time.fixedDeltaTime;
    }


    private void OnTriggerEnter(Collider PlayerHopefully)
    {
        if (PlayerHopefully.gameObject.tag == "Player")
        {
            //{call collision event}
            PlayerHopefully.GetComponent<PlayerRacer>().Fuel += 100;
            Destroy(this.gameObject);
        }
        else
        {
            //Debug.Log("collision was not player");
        }
        
    }




}
