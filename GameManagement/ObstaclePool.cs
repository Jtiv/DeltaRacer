using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePool : GenericPool<Obstacle>
{
    public void BatchPool(int count)
    {
        SpawnToPool(count);
    }
}
