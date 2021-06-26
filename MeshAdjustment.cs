using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshAdjustment : MonoBehaviour
{
    Mesh mesh;
    NoiseController NoiseController;
    MeshCollider meshCollider;

    [SerializeField]
    private GameObject gravityField;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        NoiseController = new NoiseController();
    }

    private void Start()
    {
        Mesh gravMesh = gravityField.GetComponent<MeshFilter>().mesh;

        Vector3[] Vertices = new Vector3[mesh.vertices.Length];

        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            float adjustment = NoiseController.SampleAtPosition(mesh.vertices[i]);
            
            Vertices[i] = mesh.vertices[i] * (1+adjustment);
        }

        mesh.vertices = Vertices;
        mesh.RecalculateNormals();

        gravMesh.vertices = Vertices;
        gravMesh.RecalculateNormals();

        //add colliders here so they're the right shape
        meshCollider = gameObject.AddComponent<MeshCollider>();
        var GMC = gravityField.AddComponent<MeshCollider>();
        GMC.convex = true;
        GMC.isTrigger = true;


    }



}
