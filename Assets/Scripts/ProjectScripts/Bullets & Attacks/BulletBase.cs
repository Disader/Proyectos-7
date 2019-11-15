using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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

    [Header("Partículas (PlaceHolders)")]
    public GameObject hitWallPart;
    public GameObject hitCharacterPart;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * m_velocity;
        ZoneManager.Instance.AddBulletInActiveRoom(this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisionIsEnemy = collision.GetComponent<EnemyHealth>();
        collisionIsPlayer = collision.GetComponent<PlayerControl_MovementController>();

        if (collisionIsPlayer != null && GameManager.Instance.ActualPlayerController == collisionIsPlayer) //Si es el jugador, generar camera shake
        {
            Debug.Log("playerimpact");
            CinemachineImpulseSource impulse = GetComponent<CinemachineImpulseSource>();
            impulse.GenerateImpulse();
           
        }
        if(collisionIsEnemy != null && collision.GetComponent<RoomManager>() == null)   ////Colisión con Enemigo
        {
            collisionIsEnemy.ReceiveDamage(bulletDamageToEnemy);
            Instantiate(hitCharacterPart, transform.position, transform.rotation);  //PLACEHOLDER
            ZoneManager.Instance.RemoveBulletInActiveRoom(this);
            Destroy(gameObject);
        }

        else if(collisionIsPlayer != null && collision.GetComponent<RoomManager>() == null) ////Colisión con Player
        {
            HealthHeartsVisual.healthHeartsSystemStatic.Damage(bulletDamageToPlayer);
            Instantiate(hitCharacterPart, transform.position, transform.rotation);  //PLACEHOLDER
            ZoneManager.Instance.RemoveBulletInActiveRoom(this);
            Destroy(gameObject);
        }

        else if(collision.GetComponent<RoomManager>() == null)  ////Colisión con cualquier cosa que no sea las anteriores
        {
            Instantiate(hitWallPart, transform.position, transform.rotation);   //PLACEHOLDER
            ZoneManager.Instance.RemoveBulletInActiveRoom(this);
            Destroy(gameObject);
        }
    }
}
