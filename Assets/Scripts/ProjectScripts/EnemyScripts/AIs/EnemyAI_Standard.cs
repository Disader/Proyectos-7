using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI_Standard : MonoBehaviour
{
    [SerializeField] float m_playerDetectionDistance;
    [SerializeField] LayerMask m_sightCollisionMask;
    [SerializeField] Transform m_childSprite;
    float m_originalStoppingDistance;

    NavMeshAgent m_AI_Controller;
    ShootingScript m_shootingScript;
    private void Start()
    {
        m_AI_Controller = GetComponent<NavMeshAgent>();
        m_shootingScript = GetComponent<ShootingScript>();
        m_originalStoppingDistance = m_AI_Controller.stoppingDistance;
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
            AttackingBehabiour();
        }
    }
    private void LateUpdate()
    {
        Aim();
    }
    void Aim()
    {
        if (IsPlayerInSight())
        {
            var newRot = Quaternion.LookRotation(VectorToPlayer() + GameManager.Instance.ActualPlayerController.gameObject.GetComponent<Rigidbody2D>().velocity.normalized*2f); //Cálculo de pos de disparo
            transform.rotation = Quaternion.Lerp(transform.rotation, newRot, 0.1f); //Rotación de enemigo
        }  
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
    private void FindNewDestination()
    {
        m_AI_Controller.destination = GameManager.Instance.ActualPlayerController.transform.position;
    }
    protected virtual void DamagePlayer()
    {
        m_shootingScript.FireToPlayer();
    }
    protected virtual void AttackingBehabiour()
    {
        if (IsPlayerInSight())
        {
            m_AI_Controller.stoppingDistance = m_originalStoppingDistance;
            DamagePlayer();
            if (isPlayerFurtherThanStoppingDistance())
            {
                FindNewDestination();
            }
        }
        else if (!IsPlayerInSight())
        {
            m_AI_Controller.stoppingDistance = 0.5f;           
            FindNewDestination();
        }
    }
}
