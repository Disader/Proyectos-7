using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI_Healer : EnemyAI_Standard
{
    NavMeshPath path;

    private void OnEnable()
    {
        currentAIState = AIState.idle;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (currentAIState == AIState.idle)
        {
            Idle();
            if (DistanceToPlayer() <= m_runAwayDistance)
            {
                currentAIState = AIState.runningAway;
            }
        }

        else if(currentAIState == AIState.runningAway)
        {
            MoveAwayFromPlayer();

            if(DistanceToPlayer() > m_runAwayDistance)
            {
                currentAIState = AIState.idle;
            }
        }

        else if(currentAIState == AIState.surrender)
        {
            Debug.Log("Surrender");
        }
    }

    protected override void FindNewDestination(Vector3 newDestinationPosition)
    {
        path = new NavMeshPath();
        m_AI_Controller.CalculatePath(newDestinationPosition, path);

        if (path.status == NavMeshPathStatus.PathComplete)
        {
            Debug.Log("Move");
            m_AI_Controller.path = path;           
        }

        else
        {
            currentAIState = AIState.surrender;
        }
    }

    void MoveAwayFromPlayer()
    {
        Vector2 oppositeDirectionVector = -VectorToPlayer().normalized * 2;
        Vector3 newDestination = new Vector3(oppositeDirectionVector.x + transform.position.x, oppositeDirectionVector.y + transform.position.y, 0);
        FindNewDestination(newDestination);
    }
}
