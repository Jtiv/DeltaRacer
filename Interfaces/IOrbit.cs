using UnityEngine;

public interface IOrbit
{
    bool HasGravDir();
    void SetGravDir(Vector3 centerMass, float gravModifier);
    void ExitGrav();
}

