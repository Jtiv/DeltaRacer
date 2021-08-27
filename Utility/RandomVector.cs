using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomVector
{
    public static Vector3 RandomVector3(int range)
    {
        Vector3 randVec3 = new Vector3(Random.Range(-range, range), Random.Range(-range, range), Random.Range(-range, range));
        return randVec3;
    }

    public static Vector2 RandomVector2(int range)
    {
        Vector2 randVec2 = new Vector2(Random.Range(-range, range), Random.Range(-range, range));
        return randVec2;
    }
}
