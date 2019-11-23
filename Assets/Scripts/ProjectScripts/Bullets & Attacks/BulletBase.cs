using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BulletBase : DamageObject
{
    [Header("Variables de las Balas")]
    [SerializeField] float m_velocity;
   
    [Header("Partículas (PlaceHolders)")]
    public GameObject hitWallPart;
    public GameObject hitCharacterPart;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * m_velocity;
        ZoneManager.Instance.AddBulletInActiveRoom(this);
    }
    protected override void CollisionWithEnemyEffects()
    {
        Destroy(gameObject);
        ZoneManager.Instance.RemoveBulletInActiveRoom(this);

        //Partículas
        Instantiate(hitCharacterPart, transform.position, transform.rotation);  //PLACEHOLDER

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
    }
    protected override void CollisionWithPlayerEffects()
    {
        Destroy(gameObject);

        //Partículas
        Instantiate(hitCharacterPart, transform.position, transform.rotation);  //PLACEHOLDER

        /////CAMERA SHAKE///////
        CinemachineImpulseSource impulse = GetComponent<CinemachineImpulseSource>();//PLACEHOLDER
        impulse.GenerateImpulse();//PLACEHOLDER

        ///Sonido
        MusicManager.Instance.PlaySound(AppSounds.PLAYER_HIT); ///// PLACEHOLDER


        ZoneManager.Instance.RemoveBulletInActiveRoom(this);
    }
    protected override void CollisionWithOtherEffects()
    {
        Destroy(gameObject);
        Instantiate(hitWallPart, transform.position, transform.rotation);   //PLACEHOLDER
        ZoneManager.Instance.RemoveBulletInActiveRoom(this);
    }
}
