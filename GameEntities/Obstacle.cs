using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Floatable
{
    [SerializeField]
    private GameObject spawnable;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    private void OnCollisionEnter(Collision PlayerHopefully)
    {
        if (PlayerHopefully.gameObject.tag == "Player")
        {
            int rand = Random.Range(2,4);
            for (int i = 0; i < rand; i++)
            {
                Instantiate(spawnable, transform.position, new Quaternion(rand,rand,rand,rand));
            }
            
            //{call collection event}
            ObstaclePool.Instance.ReturnToPool(this);
            
        }
        else
        {
            Debug.Log("collision was not player");
        }
        
    }

    

}
