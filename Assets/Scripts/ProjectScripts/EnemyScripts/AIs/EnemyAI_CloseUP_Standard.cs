using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_CloseUP_Standard : EnemyAI_Standard
{

    [Header("Tick SOLO si el enemigo patrulla")]
    [Space(10)]
    public bool hasPatrolBehaviour;
    public List<Transform> patrolPointsList = new List<Transform>();
    private Transform currentListPoint;
    private bool patrolPointDecided = false;

    public float timeToWaitOnPatrolPoint;
    private float timerWaitBetweenPatrolPoint = 0;

    private Vector3 lastSeenPosition;

    // Update is called once per frame
    protected override void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, VectorToPlayer(), DistanceToPlayer(), m_sightCollisionMask);
        Debug.Log(hit.collider);
        Debug.Log(IsPlayerInSight());

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

        if(currentAIState == AIState.patrol)
        {
            m_AI_Controller.stoppingDistance = 0.1f;

            if (!patrolPointDecided)
            {
                CheckPatrolPoint();
            }

            else if(patrolPointDecided)
            {
                CheckDistanceToPatrolPoint();
            }

            if(IsPlayerInSight())
            {
                currentAIState = AIState.attacking;
                patrolPointDecided = false;
            }
        }

        else if (currentAIState == AIState.attacking)
        {

            AttackingMovement();

            if (IsPlayerInSight() && DistanceToPlayer() <= distanceToShootPlayer)
            {
                DamagePlayer();
                Debug.Log(DistanceToPlayer());
            }
            if (IsPlayerTooNear())
            {
                currentAIState = AIState.runningAway;
            }

            if(!IsPlayerInSight())
            {
                currentAIState = AIState.patrol;
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

    int i = 0;

    void CheckPatrolPoint()
    {
        if (i >= patrolPointsList.Count)
        {
            i = 0;
        }

        currentListPoint = patrolPointsList[i];
        FindNewDestination(currentListPoint.position);
        i++;
        patrolPointDecided = true;
    }

    void CheckDistanceToPatrolPoint()
    {
        if(Vector3.Distance(transform.position, currentListPoint.position) <= 0.2f)
        {
            timerWaitBetweenPatrolPoint += 1 * Time.deltaTime;

            if (timerWaitBetweenPatrolPoint >= timeToWaitOnPatrolPoint)
            {
                patrolPointDecided = false;
                timerWaitBetweenPatrolPoint = 0;
            }
        }
    }
}
