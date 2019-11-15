using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_CloseUP_Standard : EnemyAI_Standard
{

    [Header("Tick SOLO si el enemigo patrulla")]
    [Space(10)]
    public bool hasPatrolBehaviour;
    public List<Transform> patrolPointsList = new List<Transform>();
    private int currentListPoint;

    // Update is called once per frame
    protected override void Update()
    {
        if (currentAIState == AIState.idle)
        {
            Idle();
            if (!hasPatrolBehaviour)
            {
                if (DetectPlayerInIdle())
                {
                    currentAIState = AIState.attacking;
                }
            }

            else
            {
                currentAIState = AIState.patrol;
            }
        }

        else if (currentAIState == AIState.attacking)
        {
            m_clockTimer += Time.deltaTime;
            if (m_clockTimer > m_clockDelay)
            {
                AttackingMovement();
                m_clockTimer = 0;
            }
            if (IsPlayerInSight() && DistanceToPlayer() <= distanceToShootPlayer)
            {
                DamagePlayer();
            }
            if (IsPlayerTooNear())
            {
                currentAIState = AIState.runningAway;
            }
        }

        else if (currentAIState == AIState.runningAway)
        {
            RunAway();
            if (!IsPlayerTooNear())
            {
                currentAIState = AIState.attacking;
            }
        }
        Aim();

        //Seteo de animaciones
        SetAnimationsVariables();
    }

    void PatrolBehaviour()
    {
        for(int i = 0; i < patrolPointsList.Count; i++)
        {

        }
    }
}
