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

    [Header("Destroy de las balas")]
    [SerializeField] float timeToDestroy;
    [SerializeField] float distanceToDestroy;
    float timerToDestroy;
    float distanceTravelled;
    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * m_velocity;
        ZoneManager.Instance.AddBulletInActiveRoom(this);
    }
    private void Update()
    {
        if (timerToDestroy != 0)
        {
            timerToDestroy += Time.deltaTime;
            if (timeToDestroy < timerToDestroy)
            {
                DestroyBullet();
            }
        }
        if (distanceToDestroy != 0)
        {
            distanceTravelled += m_velocity * Time.deltaTime;
            if (distanceToDestroy < distanceTravelled)
            {
                DestroyBullet();
            }
        } 
    }
    protected override void CollisionWithEnemyEffects()
    {
        DestroyBullet();

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
        //Partículas
        Instantiate(hitCharacterPart, transform.position, transform.rotation);  //PLACEHOLDER

        /////CAMERA SHAKE///////
        CinemachineImpulseSource impulse = GetComponent<CinemachineImpulseSource>();//PLACEHOLDER
        impulse.GenerateImpulse();//PLACEHOLDER

        ///Sonido
        MusicManager.Instance.PlaySound(AppSounds.PLAYER_HIT); ///// PLACEHOLDER

        DestroyBullet();
    }
    protected override void CollisionWithOtherEffects()
    {
        DestroyBullet();
        Instantiate(hitWallPart, transform.position, transform.rotation);   //PLACEHOLDER
    }
    void DestroyBullet()
    {
        Destroy(gameObject);
        ZoneManager.Instance.RemoveBulletInActiveRoom(this);
    }
}
