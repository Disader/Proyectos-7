using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Bomber : EnemyAI_Standard
{
    protected override void AttackingMovement()
    {
        FindNewDestination(GameManager.Instance.ActualPlayerController.transform.position);
        if (!isPlayerFurtherThanStoppingDistance())
        {
            DamagePlayer();
        }
    }
    protected override void DamagePlayer()
    {
       
        Debug.Log("olakes");
    }
}
