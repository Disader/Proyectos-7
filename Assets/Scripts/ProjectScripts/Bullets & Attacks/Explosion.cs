﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Explosion : DamageObject
{
    [SerializeField] ParticleSystem particles;
    float timer=0;
    // Update is called once per frame

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
