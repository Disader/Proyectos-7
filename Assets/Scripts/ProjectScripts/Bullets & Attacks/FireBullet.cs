using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : BulletBase
{
    [Header("Daño por fuego")]
    [SerializeField] int damagePerSecond;
    [SerializeField] float timeInFire;

    protected override void CollisionWithEnemy(Collider2D enemy)
    {
        base.CollisionWithEnemy(enemy);
        if (collisionIsEnemy.actualReciveDamageCoroutine != null) //Eliminar la anterior coroutina para que no pueda estar quemado varias veces
        {
            collisionIsEnemy.StopCoroutine(collisionIsEnemy.actualReciveDamageCoroutine);
        }
        collisionIsEnemy.actualReciveDamageCoroutine = collisionIsEnemy.StartCoroutine(collisionIsEnemy.recieveDamageOverTime(damagePerSecond, timeInFire));
    }
    protected override void CollisionWithPlayer()
    {
        base.CollisionWithPlayer();
        PlayerFireBehaviour playerFire = collisionIsPlayer.GetComponent<PlayerFireBehaviour>();

        playerFire.StopBurningPlayer();
        playerFire.actualReciveDamageCoroutine = playerFire.StartCoroutine(playerFire.recieveDamageOverTime(damagePerSecond, timeInFire)); 
    }
}
