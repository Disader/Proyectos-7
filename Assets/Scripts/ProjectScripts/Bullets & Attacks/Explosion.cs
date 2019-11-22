using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    float timer=0;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (particles.main.duration<timer)
        {
            Destroy(gameObject);
        }
    }
}
