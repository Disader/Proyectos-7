using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Bomber : EnemyAI_Standard
{
    protected override void AttackingMovement()
    {
        FindNewDestination();
        if (m_AI_Controller.remainingDistance < 1f)
        {
            DamagePlayer();
        }
    }
    protected override void DamagePlayer()
    {
        Debug.Log("Kabooom");
    }
}
