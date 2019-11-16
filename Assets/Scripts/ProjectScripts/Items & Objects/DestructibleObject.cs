using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public Sprite destroyed;
    SpriteRenderer m_rend;
    public ParticleSystem breakParticles;
    Animator m_anime;
    BoxCollider2D m_collider;
    // Start is called before the first frame update
    void Start()
    {
        m_collider = GetComponent<BoxCollider2D>();
        m_anime = GetComponent<Animator>();
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
            m_anime.SetTrigger("Hit");
            breakParticles.gameObject.SetActive(true);
            m_rend.sprite = destroyed;
            StartCoroutine (PausedParticles());
            gameObject.layer = 13;
           // m_collider.enabled = false;
        }
    }
    private IEnumerator PausedParticles()
    {

        yield return new WaitForSeconds(0.75f);
        breakParticles.Pause();

    }
}
