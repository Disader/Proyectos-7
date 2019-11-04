using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public Sprite destroyed;
    SpriteRenderer m_rend;
    public ParticleSystem breakParticles;
    // Start is called before the first frame update
    void Start()
    {
        m_rend = GetComponent<SpriteRenderer>();
        StartCoroutine(PausedParticles());
    }

    // Update is called once per frame
    void Update()
    {
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 12)
        {
            m_rend.sprite = destroyed;
            StartCoroutine (PausedParticles());
        }
    }
    private IEnumerator PausedParticles()
    {

        yield return new WaitForSeconds(0.5f);
        breakParticles.Pause();

    }
}
