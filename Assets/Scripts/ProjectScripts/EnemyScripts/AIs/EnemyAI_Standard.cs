using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI_Standard : MonoBehaviour
{
    [Header("Variables de sentidos")]
    [SerializeField] float m_playerDetectionDistance; //Para salir de idle
    [SerializeField] float m_runAwayDistance; //Cuánto se tiene que acercar el jugador para que comience a huir
    [SerializeField] LayerMask m_sightCollisionMask;
    float m_originalStoppingDistance;
    [Header("Variables de disparo al jugador")]
    [SerializeField] Transform m_armTransform;
    [SerializeField] float m_distanceAimAheadPlayer;
    [Header("Reloj de búsqueda al jugador")]
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
            if (IsPlayerInSight())
            {
                DamagePlayer();
            }
            if (IsPlayerTooNear())
            {
                currentAIState = AIState.runningAway;
            }
        }else if(currentAIState == AIState.runningAway)
        {
            RunAway();
            if (!IsPlayerTooNear())
            {
                currentAIState = AIState.attacking;
            }
        }
        Aim();
    }

    enum AIState
    {
        idle,
        attacking,
        runningAway
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
