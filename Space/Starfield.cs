using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starfield : MonoBehaviour
{

   [SerializeField]
    private ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particles;

    private int NumOfStars = 5000;
    public bool Ticked;

    private void Start()
    {
        particleSystem = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        particleSystem.SetParticles(particles, NumOfStars);
        NumOfStars = particleSystem.particleCount;
        if (Ticked == false)
        {
            CreateParticles();
        }

    }

    void CreateParticles()
    {
        NumOfStars = particleSystem.main.maxParticles;
        particles = new ParticleSystem.Particle[NumOfStars];

        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].position = new Vector3(Random.Range(-400f, 400f), Random.Range(-400f, 400f), Random.Range(-400f, 400f));
            particles[i].velocity = new Vector3(0, 0, 0);
            particles[i].startColor = new Color(1, 1, 1, 1);
            particles[i].startSize = .5f;
        }

        Ticked = true;
    }
}
