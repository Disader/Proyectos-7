using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public Sprite destroyed;
    SpriteRenderer m_rend;
    // Start is called before the first frame update
    void Start()
    {
        m_rend = GetComponent<SpriteRenderer>();
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
        }
    }
}
