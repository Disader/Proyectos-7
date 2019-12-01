using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Explosion : DamageObject
{
    [SerializeField] ParticleSystem particles;
    float timer=0;
    private void Start()
    {
        MusicManager.Instance.PlaySound(AppSounds.EXPLOSION_1,0.5f);
        MusicManager.Instance.PlaySound(AppSounds.EXPLOSION_2, 0.5f);
    }

    void Update()
    {  /////CAMERA SHAKE///////
        CinemachineImpulseSource impulse = GetComponent<CinemachineImpulseSource>();//PLACEHOLDER
        impulse.GenerateImpulse();//PLACEHOLDER

        timer += Time.deltaTime;
        if (particles.main.duration<timer)
        {
            Destroy(gameObject);
        }
    }

    protected override void CollisionWithOther(Collider2D otherObject)  //La explosión chequea en colision with other si es un bloqueo de explosión
    {
        base.CollisionWithOther(otherObject);

        ExplosionBlockade isOtherAnExplosionBlockade = otherObject.GetComponent<ExplosionBlockade>();

        if (isOtherAnExplosionBlockade != null)
        {
            isOtherAnExplosionBlockade.BreakBlockade();
        }
    }
}
