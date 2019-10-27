using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI_Standard : MonoBehaviour
{
    [SerializeField] float m_distanceToPlayer;
    [SerializeField] float m_playerDetectionDistance;
    [SerializeField] LayerMask m_sightCollisionMask;

    [SerializeField] Transform m_childSprite;

    NavMeshAgent m_AI_Controller;
    private void Start()
    {
        m_AI_Controller = GetComponent<NavMeshAgent>();
    }
    enum AIState
    {
        idle,
        attackingPlayer
    }

    AIState currentAIState = AIState.idle;

    // Update is called once per frame
    void Update()
    {
        if (currentAIState == AIState.idle)
        {
            DetectPlayer();
        }
        else if(currentAIState == AIState.attackingPlayer)
        {
            Movement();
        }      
        //Asegurarse de girar al final, tras haber hecho los cálculos del pathfinding
        m_childSprite.rotation=Quaternion.Euler(new Vector3(0, 0, 0));
    }
    void DetectPlayer()
    {
        Vector2 vectorToPlayer = GameManager.Instance.ActualPlayerController.transform.position - this.transform.position;
        float distanceToPlayer= vectorToPlayer.magnitude;
        bool hasPlayerOnSight = !Physics2D.Raycast(transform.position, vectorToPlayer, distanceToPlayer, m_sightCollisionMask);
       
        Debug.DrawRay(transform.position, vectorToPlayer.normalized * distanceToPlayer, Color.red,1f);
        if (distanceToPlayer<m_playerDetectionDistance||hasPlayerOnSight)
        {
           currentAIState = AIState.attackingPlayer;
        }
    }
    float m_AIClockNewDestination=2;

    void Movement()
    {
        m_AIClockNewDestination += Time.deltaTime;
        if (m_AIClockNewDestination > 4f || m_AI_Controller.remainingDistance < m_AI_Controller.stoppingDistance)
        {
            m_AIClockNewDestination = 0;
            m_AI_Controller.destination = FindDestination(m_distanceToPlayer)+ GameManager.Instance.ActualPlayerController.transform.position;
        }
    }
    private Vector3 FindDestination(float playerDistance)
    {
        Vector2 vector2 = Random.insideUnitCircle.normalized * playerDistance;
        return new Vector3(vector2.x, vector2.y, 0);
    }
}
