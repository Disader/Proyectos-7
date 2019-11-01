﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI_Standard : MonoBehaviour
{
    [SerializeField] float m_playerDetectionDistance;
    [SerializeField] LayerMask m_sightCollisionMask;
    float m_originalStoppingDistance;
    [SerializeField] float m_aimToPlayerMovement;
    [SerializeField] Transform m_armTransform;
    [SerializeField] float m_distanceAimAheadPlayer;
    [SerializeField] float m_clockDelay;
    float m_clockTimer;

    protected NavMeshAgent m_AI_Controller;
    ShootingScript m_shootingScript;
    Rigidbody2D m_playerRigidbody;

    protected void Start()
    {
        m_AI_Controller = GetComponent<NavMeshAgent>();
        m_shootingScript = GetComponent<ShootingScript>();
        m_originalStoppingDistance = m_AI_Controller.stoppingDistance;
        m_AI_Controller.updateUpAxis = false;
        m_AI_Controller.updateRotation = false;
    }
    protected void Update()
    {
        if (currentAIState == AIState.idle)
        {
            Idle();
            DetectPlayerInIdle();
        }
        else if (currentAIState == AIState.playerDetected)
        {
            m_clockTimer += Time.deltaTime;
            if (m_clockTimer > m_clockDelay)
            {
                AttackingMovement();
                m_clockTimer = 0;
            }
            if (IsPlayerInSight())
            {
                DamagePlayer();
            }
        }
        Aim();
    }

    enum AIState
    {
        idle,
        playerDetected
    }
    AIState currentAIState = AIState.idle;
    // Update is called once per frame

    protected virtual void Aim()
    {
        if (IsPlayerInSight())
        {
            Vector2 vector = VectorToPlayer() + GameManager.Instance.ActualPlayerController.gameObject.GetComponent<Rigidbody2D>().velocity.normalized * m_distanceAimAheadPlayer*DistanceToPlayer();
            float angle = Mathf.LerpAngle(m_armTransform.localEulerAngles.z, Vector2.SignedAngle(Vector2.right, vector),0.1f);

            m_armTransform.localEulerAngles = new Vector3(0,0, angle);
        }
    }
    protected void Idle()
    {

    }
    protected void DetectPlayerInIdle()
    {
        if (DistanceToPlayer() < m_playerDetectionDistance || IsPlayerInSight())
        {
            Debug.DrawRay(transform.position, VectorToPlayer().normalized * DistanceToPlayer(), Color.red, 1f);
            currentAIState = AIState.playerDetected;
        }
    }
    NavMeshPath path;
    protected virtual void FindNewDestination()
    {
        NavMeshPath path = new NavMeshPath();
        m_AI_Controller.CalculatePath(GameManager.Instance.ActualPlayerController.transform.position, path);
        m_AI_Controller.path = path;
    }
    protected virtual void DamagePlayer()
    {
        m_shootingScript.FireInShootingPos();
    }
    protected virtual void AttackingMovement()
    {
        if (IsPlayerInSight())
        {
            m_AI_Controller.stoppingDistance = m_originalStoppingDistance;
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

    //Funciones de referencia del jugador
    Vector2 VectorToPlayer()
    {
        return GameManager.Instance.ActualPlayerController.transform.position - this.transform.position;
    }
    float DistanceToPlayer()
    {
        return VectorToPlayer().magnitude;
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
        }
        else return false;
    }
}
