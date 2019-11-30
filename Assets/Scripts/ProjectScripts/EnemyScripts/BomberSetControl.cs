using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberSetControl : EnemySetControl
{
    EnemyAI_Bomber bomber_AI;
    private void Awake()
    {
        bomber_AI = GetComponent<EnemyAI_Bomber>();
    }
    public override IEnumerator StunEnemy()
    {
        if (!stunPart.isPlaying) //PLACEHOLDER
        {
            stunPart.Play();
        }

        yield return new WaitForSeconds(timeStunned);

        CheckEnemyDeath();
    }
    public override void CheckEnemyDeath()
    {
        base.CheckEnemyDeath();
        bomber_AI.InstantiateExplosion();
    }
}
