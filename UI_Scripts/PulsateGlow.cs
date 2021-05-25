using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsateGlow : MonoBehaviour
{
    private Material _material;

    public float intensity = 3;
    [SerializeField]
    private float _fluxSpeed;

    //Awake is called before Start
    void Start()
    {
        _material = gameObject.GetComponent<Renderer>().material;
        
        
    }

  

    // Update is called once per frame
    public void SetEmitOn()
    {
        _material.EnableKeyword("_EMISSION");
    }

   
}
