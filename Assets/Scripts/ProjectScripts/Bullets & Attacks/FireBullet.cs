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
    protected override void CollisionWithPlayer(PlayerHealthController playerHealth)
    {
        base.CollisionWithPlayer(playerHealth);

        playerHealth.StopBurningPlayer();
        playerHealth.actualReciveDamageCoroutine = playerHealth.StartCoroutine(playerHealth.RecieveDamageOverTime(damagePerSecond, timeInFire)); 
    }
    protected override void CollisionWithOther(Collider2D otherObject)  //La explosión chequea en colision with other si es un bloqueo de explosión
    {
        base.CollisionWithOther(otherObject);

        FireBlockade isOtherAFireBlockade = otherObject.GetComponent<FireBlockade>();

        if (isOtherAFireBlockade != null)
        {
            isOtherAFireBlockade.BreakBlockade();
        }
    }
}
