using UnityEngine;

public class CompassNeedle : MonoBehaviour
{
    //Control orientation of UI Object CompassNeedle making sure it always points toward the center of gravity
    //for the last gravField entered

    private PlayerRacer player;

    //functional Variables
    Vector3 planetCenterOfGravity;
    Vector3 PlayerGravDirection;

    private void Start()
    {
        player = PlayerRacer.instance;
    }

    private void LateUpdate()
    {
        Vector3 direction = (player.GetPlanetCenterMass() - player.transform.position);
        Quaternion rotation = Quaternion.LookRotation(direction, player.transform.forward);
        transform.rotation = rotation;
    }
}
