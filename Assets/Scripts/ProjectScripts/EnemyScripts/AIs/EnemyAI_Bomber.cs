using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Bomber : EnemyAI_Standard
{
    protected override void AttackingMovement()
    {
        FindNewDestination(GameManager.Instance.ActualPlayerController.transform.position);
    }
    protected override void DamagePlayer()
    {
        m_shootingScript.FireInShootingPos(ShootingScript.whoIsShooting.enemy);
        Destroy(gameObject);
    }
}
