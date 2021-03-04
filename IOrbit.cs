using UnityEngine;

public interface IOrbit
{
    void SetGravDir(Vector3 centerMass, float gravModifier);
    void ResetGravDir();
}

