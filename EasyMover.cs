using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyMover : MonoBehaviour
{
    [SerializeField]
    float Tspeed;
    Rigidbody rb;

    [SerializeField]
    float MoveSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    public void MoveToward(Transform Target)
    {
        if (Target != null)
        {


            Vector3 Direction = Target.position - gameObject.transform.position;
            Vector3 NewFwd = Vector3.RotateTowards(transform.forward, Direction, Tspeed * Time.deltaTime, 0.0f);
            var read = Quaternion.LookRotation(NewFwd);
            transform.rotation = Quaternion.Lerp(transform.rotation, read, Time.deltaTime);
            //rb.AddForce(transform.forward * MoveSpeed * Time.deltaTime);
            if (Mathf.Abs(Quaternion.Dot(transform.rotation, read)) > .97f)
            {
                rb.AddForce(Direction * MoveSpeed * Time.deltaTime);
            }

        }
        else
        {
            rb.velocity *= .99f;
        }
        
    }


   

}
