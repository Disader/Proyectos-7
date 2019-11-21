using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_CloseUP_Standard : MonoBehaviour
{
   /* [Header("Posición inicial para Enemigos SIN patrulla")]
    private Vector3 initialPosition;

    [Header("Tick SOLO si el enemigo patrulla")]
    [Space(10)]
    public bool hasPatrolBehaviour;
    public List<Transform> patrolPointsList = new List<Transform>();
    private Transform currentListPoint;
    private bool patrolPointDecided = false;

    public float timeToWaitOnPatrolPoint;
    private float timerWaitBetweenPatrolPoint = 0;

    [Header("Variables para ir a última posición de jugador")]
    private bool lastPositionChecked;
    private Vector3 lastSeenPosition;
    public float timeToWaitOnLastPosition;
    private float timerWaitLastPos;

    protected override void Start()
    {
        base.Start();

        initialPosition = transform.position;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (currentAIState == AIState.idle)
        {
            Idle();
            if (!hasPatrolBehaviour)
            {
                if (IsPlayerInSight())
                {
                    currentAIState = AIState.attacking;
                }
            }

            else
            {
                currentAIState = AIState.patrol;
            }
        }

        if(currentAIState == AIState.patrol) ///Estado de patrulla
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

        if(currentAIState == AIState.goLastSeenPlace) ///Estado de buscar al jugador en la última posición visto
        {
            m_AI_Controller.stoppingDistance = 0.2f;

            if (!lastPositionChecked)
            {
                CheckPlayerLastPosition();
            }

            else if(lastPositionChecked)
            {
                CheckDistanceToLastPosition();
            }

            if (IsPlayerInSight())
            {
                currentAIState = AIState.attacking;
                m_AI_Controller.autoBraking = false;
                lastPositionChecked = false;
            }
        }

        else if (currentAIState == AIState.attacking)
        {

            AttackingMovement();

            if (IsPlayerInSight() && DistanceToPlayer() <= distanceToShootPlayer)
            {
                DamagePlayer();
            }
            if (IsPlayerTooNear())
            {
                currentAIState = AIState.runningAway;
            }

            if(!IsPlayerInSight())
            {
                currentAIState = AIState.goLastSeenPlace;
            }

            if(GameManager.Instance.playerIsHidden) ///Si el player entra en tunel
            {
                currentAIState = AIState.goLastSeenPlace;
                m_AI_Controller.autoBraking = true;
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

    void CheckPatrolPoint() ///Chequeo de a que punto de patrulla ir, se llama en estado Patrol
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

    void CheckDistanceToPatrolPoint()   ///Chequeo de la distancia hasta el punto de patrulla
    {
        if(Vector3.Distance(transform.position, currentListPoint.position) <= m_AI_Controller.stoppingDistance + 0.1f) ///El stopping distance +0.1f es para checkear la posicion en la que se para
        {
            timerWaitBetweenPatrolPoint += 1 * Time.deltaTime;

            if (timerWaitBetweenPatrolPoint >= timeToWaitOnPatrolPoint) ///Una vez pasado cierto tiempo en el punto, se chequea el siguiente punto
            {
                patrolPointDecided = false;
                timerWaitBetweenPatrolPoint = 0;
            }
        }
    }

    void CheckPlayerLastPosition()
    {
        lastSeenPosition = GameManager.Instance.ActualPlayerController.transform.position;
        FindNewDestination(lastSeenPosition);
        lastPositionChecked = true;
    }

    void CheckDistanceToLastPosition()
    {
        if (Vector3.Distance(transform.position,lastSeenPosition) <= m_AI_Controller.stoppingDistance + 0.1f) ///El stopping distance +0.1f es para checkear la posicion en la que se para
        {
            timerWaitLastPos += 1 * Time.deltaTime;

            if(timerWaitLastPos >= timeToWaitOnLastPosition)
            {
                timerWaitLastPos = 0;

                if (hasPatrolBehaviour)
                {
                    currentAIState = AIState.patrol;
                    m_AI_Controller.autoBraking = false;
                }

                else
                {
                    FindNewDestination(initialPosition);
                    currentAIState = AIState.idle;
                    m_AI_Controller.autoBraking = false;
                }
            }
        }
    }*/
}
