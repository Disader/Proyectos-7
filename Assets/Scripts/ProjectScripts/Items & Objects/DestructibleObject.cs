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
    [SerializeField]
    bool isExplosive;
    [SerializeField]
    GameObject m_explosion;
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
 
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BulletBase>() != null)
        {
            Destroy();
        }
    }
    private IEnumerator PausedParticles()
    {

        yield return new WaitForSeconds(0.75f);
        breakParticles.Pause();

    }
    public void Destroy()
    {
        print(GetInstanceID());
        
        m_anime.SetTrigger("Hit");
        breakParticles.gameObject.SetActive(true);
        
        StartCoroutine(PausedParticles());
        gameObject.layer = 13;
        // m_collider.enabled = false;
        if(isExplosive)
        {
          GameObject exp =  Instantiate(m_explosion, transform.position, transform.rotation);
            exp.GetComponentInChildren<ParticleSystem>().gameObject.SetActive(false);
            exp.GetComponent<Animator>().enabled = false; 
            exp.GetComponent<SpriteRenderer>().enabled = false;
            exp.gameObject.transform.localScale /= 2;
        }
    }
}
