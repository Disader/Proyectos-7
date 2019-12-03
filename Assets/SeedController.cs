using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SeedController : MonoBehaviour
{
    ParticleSystem m_particles;
    public int seed;
     
    // Start is called before the first frame update
    void Awake()
    {
        uint lel = (uint)seed;
        m_particles = GetComponent<ParticleSystem>();
        m_particles.randomSeed = lel;
        m_particles.useAutoRandomSeed = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
