﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI_Standard : MonoBehaviour
{
    [Header("Variables de sentidos")]
    [SerializeField] protected float m_playerDetectionDistance; //Para salir de idle
    [SerializeField] protected float m_runAwayDistance; //Cuánto se tiene que acercar el jugador para que comience a huir
    [SerializeField] LayerMask m_sightCollisionMask;
    float m_originalStoppingDistance;
    [Header("Variables de disparo al jugador")]
    [SerializeField] protected Transform m_armTransform;
    [SerializeField] float m_distanceAimAheadPlayer;
    [Header("La distancia mínima para disparar al jugador")]
    [SerializeField] protected float distanceToShootPlayer; ///Una distancia mínima para disparar
    [Header("Reloj de búsqueda al jugador")]
    [SerializeField] protected float m_clockDelay;
    protected float m_clockTimer;
    [Header("Animators")]
    [SerializeField] protected Animator characterAnimator;

    protected NavMeshAgent m_AI_Controller;
    ShootingScript m_shootingScript;
    Rigidbody2D m_playerRigidbody;
    protected Rigidbody2D m_myRigidbody;

    protected void Start()
    {
        m_AI_Controller = GetComponent<NavMeshAgent>();
        m_shootingScript = GetComponent<ShootingScript>();
        m_myRigidbody = GetComponent<Rigidbody2D>();
        m_originalStoppingDistance = m_AI_Controller.stoppingDistance;
        m_AI_Controller.updateUpAxis = false;
        m_AI_Controller.updateRotation = false;
    }
    protected virtual void Update()
    {

        if (currentAIState == AIState.idle)
        {
            Idle();
            if (DetectPlayerInIdle())
            {
                currentAIState = AIState.attacking;
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

        else if(currentAIState == AIState.runningAway)
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

    protected enum AIState
    {
        idle,
        attacking,
        runningAway,
        patrol,     //Para comportamientos de patrulla
        goLastSeenPlace, //Para comportamientos de patrulla
        surrender   //Para el Healer
    }
    protected AIState currentAIState = AIState.idle;
    protected float angle;

    protected virtual void Aim()
    {
        if (IsPlayerInSight())
        {
            Vector2 vector = VectorToPlayer() + GameManager.Instance.ActualPlayerController.gameObject.GetComponent<Rigidbody2D>().velocity.normalized * m_distanceAimAheadPlayer*DistanceToPlayer();
            angle = Mathf.LerpAngle(m_armTransform.localEulerAngles.z, Vector2.SignedAngle(Vector2.right, vector),0.1f);

            m_armTransform.localEulerAngles = new Vector3(0,0, angle);
        }
        else if (m_AI_Controller.velocity.normalized.magnitude != 0) //Si no ve al jugador y se está moviendo
        {
            angle = Vector2.SignedAngle(Vector2.right, m_AI_Controller.velocity.normalized);
            m_armTransform.localEulerAngles = new Vector3(0, 0, angle); ////Se rota el objeto de brazo para igualar la dirección del joystick en el eje Z.
        }
        else //Si NO vee al jugador y no se mueve mantiene el ángulo anterior
        {
            m_armTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
    protected bool IsPlayerTooNear()
    {
        if (DistanceToPlayer()< m_runAwayDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    protected void RunAway()
    {
        m_AI_Controller.stoppingDistance =0.1f;
        Vector2 oppositeDirectionVector = -VectorToPlayer().normalized*2;
        Vector3 newDestination = new Vector3(oppositeDirectionVector.x + transform.position.x, oppositeDirectionVector.y + transform.position.y, 0);
        FindNewDestination(newDestination);
    }
    protected void Idle()
    {

    }
    protected bool DetectPlayerInIdle()
    {
        if (DistanceToPlayer() < m_playerDetectionDistance || IsPlayerInSight())
        {
            Debug.DrawRay(transform.position, VectorToPlayer().normalized * DistanceToPlayer(), Color.red, 1f);
            return true;
        }
        else { return false; }
    }
    NavMeshPath path;
    protected virtual void FindNewDestination(Vector3 newDestinationPosition)
    {
        NavMeshPath path = new NavMeshPath();
        m_AI_Controller.CalculatePath(newDestinationPosition, path);
        m_AI_Controller.path = path;
    }
    protected virtual void DamagePlayer()
    {
        m_shootingScript.FireInShootingPos(ShootingScript.whoIsShooting.enemy);
    }
    protected virtual void AttackingMovement()
    {
        if (IsPlayerInSight())
        {
            m_AI_Controller.stoppingDistance = m_originalStoppingDistance;
            if (isPlayerFurtherThanStoppingDistance())
            {
                FindNewDestination(GameManager.Instance.ActualPlayerController.transform.position);
            }
        }
        else if (!IsPlayerInSight())
        {
            m_AI_Controller.stoppingDistance = 0.5f;           
            FindNewDestination(GameManager.Instance.ActualPlayerController.transform.position);
        }
    }

    //Funciones de referencia del jugador
    protected Vector2 VectorToPlayer()
    {
        return GameManager.Instance.ActualPlayerController.transform.position - m_armTransform.position;
    }
    protected float DistanceToPlayer()
    {
        return VectorToPlayer().magnitude;
    }
    protected bool IsPlayerInSight()
    {
        return !Physics2D.Raycast(transform.position, VectorToPlayer(), DistanceToPlayer(), m_sightCollisionMask);
    }
    protected bool isPlayerFurtherThanStoppingDistance()
    {
        if (m_AI_Controller.stoppingDistance < DistanceToPlayer())
        {
            return true;
        }
        else return false;
    }



    ///////////////////////////////////Animaciones//////////////////////////////
    protected void SetAnimationsVariables()
    {
        if (angle < 0)
        {
            angle = 360 + angle;
        }
        characterAnimator.SetFloat("Angle", angle);
        characterAnimator.SetBool("IsMoving", m_AI_Controller.velocity.magnitude!=0);
    }
}
