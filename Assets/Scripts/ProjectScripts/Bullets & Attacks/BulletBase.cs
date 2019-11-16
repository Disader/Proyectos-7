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


        if (collisionIsEnemy != null && collision.GetComponent<RoomManager>() == null)   ////Colisión con Enemigo
        {
            collisionIsEnemy.ReceiveDamage(bulletDamageToEnemy);
            ZoneManager.Instance.RemoveBulletInActiveRoom(this);

            //Partículas
            Instantiate(hitCharacterPart, transform.position, transform.rotation);  //PLACEHOLDER

            ///Sonido
            ///Sonido
            int random = Random.Range(0, 2);
            switch (random)
            {
                case 0:
                    MusicManager.Instance.PlaySound(AppSounds.ENEMY_HIT1); ///// PLACEHOLDER
                    break;
                case 1:
                    MusicManager.Instance.PlaySound(AppSounds.ENEMY_HIT2); ///// PLACEHOLDER
                    break;
                default:
                    break;
            }

            Destroy(gameObject);
        }

        else if (collisionIsPlayer != null && collisionIsPlayer==GameManager.Instance.ActualPlayerController && collision.GetComponent<RoomManager>() == null) ////Colisión con Player
        {
            HealthHeartsVisual.healthHeartsSystemStatic.Damage(bulletDamageToPlayer);

            //Partículas
            Instantiate(hitCharacterPart, transform.position, transform.rotation);  //PLACEHOLDER

            /////CAMERA SHAKE///////
            CinemachineImpulseSource impulse = GetComponent<CinemachineImpulseSource>();//PLACEHOLDER
            impulse.GenerateImpulse();//PLACEHOLDER

            ///Sonido
            MusicManager.Instance.PlaySound(AppSounds.PLAYER_HIT); ///// PLACEHOLDER


            ZoneManager.Instance.RemoveBulletInActiveRoom(this);
            Destroy(gameObject);
        }

        else if (collision.GetComponent<RoomManager>() == null)  ////Colisión con cualquier cosa que no sea las anteriores
        {
            Instantiate(hitWallPart, transform.position, transform.rotation);   //PLACEHOLDER
            ZoneManager.Instance.RemoveBulletInActiveRoom(this);
            Destroy(gameObject);
        }
    }
}
