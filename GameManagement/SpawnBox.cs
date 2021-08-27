using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBox : MonoBehaviour
{
    private Transform player;

    public float ObjectDespawnDistance = 4000f;
    public float ObjectScalingDistance = 2000f;
    public float ObjectSize = 1f;

    //internal variables
    private float objectDespawnDistanceSqrd;
    private float objectScalingDistanceSqrd;

    // Start is called before the first frame update
    void Start()
    {
        player = transform; //cache for update calls
        objectScalingDistanceSqrd = ObjectScalingDistance * ObjectScalingDistance;
        objectDespawnDistanceSqrd = ObjectDespawnDistance * ObjectDespawnDistance;
    }

    
    //experiment idea --> return a float; scale the object by the float, if the float is close enough to zero, return to pool;

    public float RadiusCheck(GameObject Planet, GameObject player)
    {
        //when the object clip distance is breached -- start to descale the object until its 0 and then return to pool
        if (Planet != null && (Planet.transform.position - player.transform.position).sqrMagnitude > objectScalingDistanceSqrd)
        {
            //but first...if the spacing distance is breached -- despawn object return to object pool
            if ((Planet.transform.position - player.transform.position).sqrMagnitude > objectDespawnDistanceSqrd)
            {
                //trigger return to pool
                Debug.Log("trigger return to pool");
                return 0;
            }
            else
            {
                //now start scaling (this is currently wrong -- will start to descale incorrectly -- needs to reflect distance outside
                //sphere 1 and inside sphere 2
                float scalePercent = (Planet.transform.position - player.transform.position).sqrMagnitude / objectDespawnDistanceSqrd;
                Debug.Log("scaling object down by " + scalePercent);
                return scalePercent;
            }
            

        }
        else
        {
            return 1;
        }
        
    }

    //other functions pertaining to radius checking, like stars...



}
