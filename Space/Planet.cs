using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    //cache this to keep for golidlocks and other mesh related functions
    public float Scale;

    private Mesh mesh;
    private Vector3[] Vertices;
    
    NoiseController NoiseController;

    [SerializeField]
    private GameObject GFprefab;

    public GravField gravfield;

    //Awake or OnEnable? OnEnable would refresh the planet every time the player gets far enough away...
    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        mesh.MarkDynamic();

        NoiseController = new NoiseController();

        var gravityFieldInstance = Instantiate(GFprefab, transform);
        
        gravityFieldInstance.transform.localScale = gameObject.transform.localScale * 1.16f;
        gravityFieldInstance.layer = 2;

        Mesh gravMesh = gravityFieldInstance.GetComponent<MeshFilter>().mesh;

        Vertices = new Vector3[mesh.vertices.Length];

        //cache the random vector3 here so that it isnt re-generated for every vertex
        Vector3 RandOffset = RandomVector.RandomVector3(30);

        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            float adjustment = NoiseController.SampleAtRandPosition(mesh.vertices[i], RandOffset);

            Vertices[i] = mesh.vertices[i] * (.7f + adjustment);
        }

        mesh.vertices = Vertices;
        mesh.RecalculateNormals();

        gravMesh.vertices = Vertices;
        gravMesh.RecalculateNormals();

        //Add colliders here so they're the right shape
        var MC = gameObject.AddComponent<MeshCollider>();
        MC.sharedMesh = mesh;
        //MC.convex = true;

        var GMC = gravityFieldInstance.AddComponent<MeshCollider>();
        GMC.sharedMesh = gravMesh;
        GMC.convex = true;
        GMC.isTrigger = true;

        var RB = gameObject.AddComponent<Rigidbody>();
        RB.mass = transform.localScale.x;
        RB.useGravity = false;
        RB.isKinematic = true;
        
        gravfield = gravityFieldInstance.GetComponent<GravField>();
        //grav should be set here to be nice and self contained but this avoids OnEnable -- hacky.
        //gravfield.SetCenterOfMass(this.transform);

    }
    
    public Vector3 GoldilocksZone()
    {
        int randomIdx = Mathf.RoundToInt(Random.Range(0, Vertices.Length));
        Vector3 point = gameObject.GetComponent<MeshFilter>().mesh.vertices[randomIdx] * 1.08f;
        point = transform.TransformPoint(point);
        return point;
    }
}
  