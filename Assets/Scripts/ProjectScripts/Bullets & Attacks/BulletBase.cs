using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    [SerializeField] float m_velocity;
    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * m_velocity;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}
