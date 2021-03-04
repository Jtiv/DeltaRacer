using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : Floatable
{ 
    
    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    private void OnCollisionEnter(Collision PlayerHopefully)
    {
        if (PlayerHopefully.gameObject.tag == "Player")
        {
            //{call collection event}
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("collision was not player");
        }
        

    }



}
