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
        
        //else { debug.log("collision was not a player (planet most likely)"); }

    }
    
    //does this ever actually set _ingravField to false? if so is it reliable and can we use it for despawning?
    void LateUpdate()
    {
        if (_inGravField == false)
        {
            CheckDespawnSelf();
        }
    }

    public IEnumerator CheckDespawnSelf()
    {
        yield return new WaitForSeconds(1);
        if (_inGravField == false)
        {
            ObstaclePool.Instance.ReturnToPool(this);
        }
    }

}
