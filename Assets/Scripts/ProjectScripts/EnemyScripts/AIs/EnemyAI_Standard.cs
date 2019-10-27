using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI_Standard : MonoBehaviour
{
    [SerializeField] float m_playerDetectionDistance;
    [SerializeField] LayerMask m_sightCollisionMask;
    [SerializeField] float m_firingRate;
    float m_firingTimer;
    [SerializeField] Transform m_childSprite;

    NavMeshAgent m_AI_Controller;
    private void Start()
    {
        m_AI_Controller = GetComponent<NavMeshAgent>();
    }
    enum AIState
    {
        idle,
        playerDetected
    }

    AIState currentAIState = AIState.idle;
    // Update is called once per frame
    void Update()
    { 
        if (currentAIState == AIState.idle)
        {
            Idle();
            DetectPlayer();
        }
        else if(currentAIState == AIState.playerDetected)
        {
            m_firingTimer += Time.deltaTime;
            if (IsPlayerInSight())
            {
                if (m_firingTimer > m_firingRate)
                {
                    Shot();
                    m_firingTimer = 0;
                }
                if (isPlayerFurtherThanStoppingDistance())
                {
                    m_AI_Controller.destination = FindNewDestination();
                }
            }else if (!IsPlayerInSight())
            {
                FindNewDestination();
            }
        }      
        //Asegurarse de girar al final, tras haber hecho los cálculos del pathfinding
        m_childSprite.rotation=Quaternion.Euler(new Vector3(0, 0, 0));
    }
    Vector2 VectorToPlayer()
    {
        return GameManager.Instance.ActualPlayerController.transform.position - this.transform.position;
    }
    float DistanceToPlayer()
    {
        return VectorToPlayer().magnitude;
    }
    void Idle()
    {

    }
    void DetectPlayer()
    {
        Debug.DrawRay(transform.position, VectorToPlayer().normalized * DistanceToPlayer(), Color.red,1f);
        if (DistanceToPlayer() < m_playerDetectionDistance|| IsPlayerInSight())
        {
           currentAIState = AIState.playerDetected;
        }
    }
    bool IsPlayerInSight()
    {
        return !Physics2D.Raycast(transform.position, VectorToPlayer(), DistanceToPlayer(), m_sightCollisionMask);
    }
    bool isPlayerFurtherThanStoppingDistance()
    {
        if (m_AI_Controller.stoppingDistance < DistanceToPlayer())
        {
            return true;
        } else return false;
    }
    private Vector3 FindNewDestination()
    {
        return GameManager.Instance.ActualPlayerController.transform.position;
    }
    void Shot()
    {
        Debug.Log("shot");
    }
}
