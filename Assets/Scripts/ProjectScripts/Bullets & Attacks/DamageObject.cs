using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    [Header("El Daño en Número de Fragmentos Perdidos al Jugador")]
    public int bulletDamageToPlayer;
    [Header("El Daño al Enemigo")]
    public int bulletDamageToEnemy;

    [Header("Variables para Comprobar Colisión")]
    protected EnemyHealth collisionIsEnemy;
    protected PlayerControl_MovementController collisionIsPlayer;

     [Header("Chequear que solo se ejecuta una vez")]
    private bool collideOnce = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisionIsEnemy = collision.GetComponent<EnemyHealth>();
        collisionIsPlayer = collision.GetComponent<PlayerControl_MovementController>();

        if (collisionIsEnemy != null && collision.GetComponent<RoomManager>() == null)   ////Colisión con Enemigo
        {
            
            CollisionWithEnemy(collision);
        }

        else if (collisionIsPlayer != null && collisionIsPlayer == GameManager.Instance.ActualPlayerController && collision.GetComponent<RoomManager>() == null) ////Colisión con Player
        {
            PlayerHealthController playerHealth = collision.GetComponent<PlayerHealthController>();
            if (playerHealth != null)
            {
                CollisionWithPlayer(playerHealth);
            }
           
        }

        else if (collision.GetComponent<RoomManager>() == null)  ////Colisión con cualquier cosa que no sea las anteriores
        {
            CollisionWithOther();
        }
    }
    protected virtual void CollisionWithEnemy(Collider2D enemy)
    {
        collisionIsEnemy.ReceiveDamage(bulletDamageToEnemy);
        EnemySetControl myEnemySetControl = enemy.GetComponent<EnemySetControl>();
        if (myEnemySetControl != null)
        {
            myEnemySetControl.StopConsumingAction();
        }
        CollisionWithEnemyEffects();
    }
    protected virtual void CollisionWithEnemyEffects()
    {

    }
    protected virtual void CollisionWithPlayer(PlayerHealthController playerHealth)
    {
        playerHealth.DamagePlayer(bulletDamageToPlayer);
        CollisionWithPlayerEffects();
    }
    protected virtual void CollisionWithPlayerEffects()
    {
    }
    protected virtual void CollisionWithOther()
    {
        CollisionWithOtherEffects();
    }
    protected virtual void CollisionWithOtherEffects()
    { 
    }
}
