using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI_Standard : MonoBehaviour
{
    [Header("Variables de sentidos")]
    [SerializeField] protected float m_playerDetectionDistance; //Para salir de idle
    [SerializeField] protected float m_runAwayDistance; //Cuánto se tiene que acercar el jugador para que comience a huir
    [SerializeField] protected LayerMask m_sightCollisionMask;
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

    [Header("Componentes")]
    protected NavMeshAgent m_AI_Controller;
    ShootingScript m_shootingScript;
    Rigidbody2D m_playerRigidbody;
    protected Rigidbody2D m_myRigidbody;

    [Header("Posición inicial para Enemigos SIN patrulla")]
    private Vector3 initialPosition;

    [Header("Tick SOLO si el enemigo patrulla")]
    [Space(10)]
    public bool hasPatrolBehaviour;
    public List<Transform> patrolPointsList = new List<Transform>();
    private Transform currentListPoint;
    private bool patrolPointDecided = false;

    public float timeToWaitOnPatrolPoint;
    private float timerWaitBetweenPatrolPoint = 0;

    /*[Header("Variables para ir a última posición de jugador")]
    private bool lastPositionChecked;
    private Vector3 lastSeenPosition;
    public float timeToWaitOnLastPosition;
    private float timerWaitLastPos;*/

    protected virtual void Start()
    {
        m_AI_Controller = GetComponent<NavMeshAgent>();
        m_shootingScript = GetComponent<ShootingScript>();
        m_myRigidbody = GetComponent<Rigidbody2D>();
        m_originalStoppingDistance = m_AI_Controller.stoppingDistance;
        m_AI_Controller.updateUpAxis = false;
        m_AI_Controller.updateRotation = false;

        initialPosition = transform.position; //Posición inicial.
    }

    // Update is called once per frame
    protected virtual void Update()
    {

        if (currentAIState == AIState.idle)
        {
            Idle();

            if(hasPatrolBehaviour)
            {
                currentAIState = AIState.patrol;
            }

            if(DetectPlayerInInitialState())
            {
                currentAIState = AIState.attacking;
            }
        }

        if (currentAIState == AIState.patrol) ///Estado de patrulla
        {
            m_AI_Controller.stoppingDistance = 0.1f;

            if (!patrolPointDecided)
            {
                CheckPatrolPoint();
            }

            else if (patrolPointDecided)
            {
                CheckDistanceToPatrolPoint();
            }

            if (DetectPlayerInInitialState())
            {
                currentAIState = AIState.attacking;
                patrolPointDecided = false;
            }
        }

        /*if (currentAIState == AIState.goLastSeenPlace) ///Estado de buscar al jugador en la última posición visto
        {
            m_AI_Controller.stoppingDistance = 0.2f;

            if (!lastPositionChecked)
            {
                CheckPlayerLastPosition();
            }

            else if (lastPositionChecked)
            {
                CheckDistanceToLastPosition();
            }

            if (IsPlayerInSight())
            {
                currentAIState = AIState.attacking;
                lastPositionChecked = false;
            }
        }*/

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

    protected enum AIState
    {
        idle,
        attacking,
        runningAway,
        patrol,     //Para comportamientos de patrulla
        goLastSeenPlace, //Para comportamientos de patrulla, si pierden al jugador
        surrender   //Para el Healer
    }
    protected AIState currentAIState = AIState.idle;
    protected float angle;

    protected virtual void Aim()
    {
        if (IsPlayerInSight())
        {
            Vector2 vector = VectorToPlayerFixedAim() + GameManager.Instance.ActualPlayerController.gameObject.GetComponent<Rigidbody2D>().velocity.normalized * m_distanceAimAheadPlayer * DistanceToPlayer();
            Debug.DrawRay(transform.position, vector);
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
    protected bool DetectPlayerInInitialState()
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

    ////////////////Añadido de EnemyCloseUp///////////////////

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
        if (Vector3.Distance(transform.position, currentListPoint.position) <= m_AI_Controller.stoppingDistance + 0.1f) ///El stopping distance +0.1f es para checkear la posicion en la que se para
        {
            timerWaitBetweenPatrolPoint += 1 * Time.deltaTime;

            if (timerWaitBetweenPatrolPoint >= timeToWaitOnPatrolPoint) ///Una vez pasado cierto tiempo en el punto, se chequea el siguiente punto
            {
                patrolPointDecided = false;
                timerWaitBetweenPatrolPoint = 0;
            }
        }
    }

    /*void CheckPlayerLastPosition()
    {
        lastSeenPosition = GameManager.Instance.ActualPlayerController.transform.position;
        FindNewDestination(lastSeenPosition);
        lastPositionChecked = true;
    }

    void CheckDistanceToLastPosition()
    {
        if (Vector3.Distance(transform.position, lastSeenPosition) <= m_AI_Controller.stoppingDistance + 0.1f) ///El stopping distance +0.1f es para checkear la posicion en la que se para
        {
            timerWaitLastPos += 1 * Time.deltaTime;

            if (timerWaitLastPos >= timeToWaitOnLastPosition)
            {
                timerWaitLastPos = 0;

                if (hasPatrolBehaviour)
                {
                    currentAIState = AIState.patrol;
                }

                else
                {
                    FindNewDestination(initialPosition);
                    currentAIState = AIState.idle;
                }
            }
        }
    }*/


    ////////////////////Funciones de referencia del jugador////////////////////////

    protected Vector2 VectorToPlayer()  ///El Vector a jugador desde la base del enemigo, para cuestiones de NAVEGACIÓN
    {
        return GameManager.Instance.ActualPlayerController.transform.position - transform.position;
    }

    protected Vector2 VectorToPlayerFixedAim()  ///El Vector a jugador desde la posición del brazo, para cuestiones de APUNTADO
    {
        return GameManager.Instance.ActualPlayerController.transform.position - m_armTransform.position;
    }

    protected float DistanceToPlayer()
    {
        return VectorToPlayer().magnitude;
    }
    protected bool IsPlayerInSight()
    {
        float angle;
        angle = Vector3.SignedAngle(VectorToPlayer(), m_armTransform.right, transform.up);

        if (!Physics2D.Raycast(transform.position, VectorToPlayer(), DistanceToPlayer(), m_sightCollisionMask) && angle < 90) //Si el raycast no tiene nada de por medio y el player está en un angulo de menos de 90º tomando como 0 la dirección del brazo, ve al jugador
        {
            return true;
        }

        else
        {
            return false;
        }
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
