using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Bomber : EnemyAI_Standard
{
    BomberSetControl m_myBomberSetControl;
    protected override void Start()
    {
        base.Start();
        m_myBomberSetControl = GetComponent<BomberSetControl>();
    }
    protected override void AttackingMovement()
    {
        FindNewDestination(GameManager.Instance.ActualPlayerController.transform.position);
    }
    public override void DamagePlayer()
    {
        m_myBomberSetControl.CheckEnemyDeath();
    }
    public void InstantiateExplosion()
    {
        m_shootingScript.FireInShootingPos(ShootingScript.whoIsShooting.enemy);
    }
}
