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
            if (hasPatrolBehaviour)
            {
                Patrol();
            }
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
        //Animaciones
        Aim();
        SetAnimationsVariables();
    }

    protected override void FindNewDestination(Vector3 newDestinationPosition)
    {
        path = new NavMeshPath();
        m_AI_Controller.CalculatePath(newDestinationPosition, path);

        if (path.status == NavMeshPathStatus.PathComplete)
        {
            m_AI_Controller.path = path;           
        }

        else
        {
            currentAIState = AIState.surrender;
        }
    }
    protected override void Aim()
    {
        if (m_AI_Controller.velocity.normalized.magnitude != 0) //Si no se controla la dirección y se está moviendo
        {
            angle = Vector2.SignedAngle(Vector2.right, m_AI_Controller.velocity.normalized);
            m_armTransform.localEulerAngles = new Vector3(0, 0, angle); ////Se rota el objeto de brazo para igualar la dirección del joystick en el eje Z.
        }
    }

    void MoveAwayFromPlayer()
    {
        Vector2 oppositeDirectionVector = -VectorToPlayer().normalized * 2;
        Vector3 newDestination = new Vector3(oppositeDirectionVector.x + transform.position.x, oppositeDirectionVector.y + transform.position.y, 0);
        FindNewDestination(newDestination);
    }

}
