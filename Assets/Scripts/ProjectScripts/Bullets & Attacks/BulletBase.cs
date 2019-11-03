using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    [Header("Variables de las Balas")]
    [SerializeField] float m_velocity;
    [Header("El Daño en Número de Fragmentos Perdidos al Jugador")]
    public int bulletDamageToPlayer;
    [Header("El Daño al Enemigo")]
    public int bulletDamageToEnemy;

    [Header("Variables para Comprobar Colisión")]
    private EnemyHealth collisionIsEnemy;
    private PlayerControl_MovementController collisionIsPlayer;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * m_velocity;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisionIsEnemy = collision.GetComponent<EnemyHealth>();
        collisionIsPlayer = collision.GetComponent<PlayerControl_MovementController>();

        if (collision.GetComponent<RoomManager>() == null)
        {
            Destroy(this.gameObject);
        } 

        if(collisionIsEnemy != null)
        {
            collisionIsEnemy.ReceiveDamage(bulletDamageToEnemy);
        }

        else if(collisionIsPlayer != null)
        {
            HealthHeartsVisual.healthHeartsSystemStatic.Damage(bulletDamageToPlayer);
        }
    }
}
