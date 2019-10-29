using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySetControl : MonoBehaviour
{
    [Header("El PlayerControl de este Enemigo")]
    private EnemyControl_MovementController this_EnemyControl_MovementController;

    [Header("La IA y el Agente del Enemigo")]
    private EnemyAI_Standard this_EnemyAI;
    private NavMeshAgent this_EnemyNavAgent;

    [Header("El PlayerControl del Jugador")]
    private PlayerControl_MovementController player_MovementController;

    [Header("RigidBodies")]
    private Rigidbody2D thisEnemyRB;

    [Header("Variables Stun")]
    public float timeStunned;

    // Start is called before the first frame update
    void Start()
    {
        this_EnemyControl_MovementController = GetComponent<EnemyControl_MovementController>();
        this_EnemyAI = GetComponent<EnemyAI_Standard>();
        this_EnemyNavAgent = GetComponent<NavMeshAgent>();
        player_MovementController = GameManager.Instance.realPlayerGO.GetComponent<PlayerControl_MovementController>();
        thisEnemyRB = GetComponent<Rigidbody2D>();
    }

    public void PosssessEnemy()  ////Se desactiva la IA y el agente, se activa el control de enemigo, se detiene el movimiento residual del control de jugador, se desactiva el objeto del jugador  y se indica a GameManager que este enmigo es ActualPlayer
    {
        StopAllCoroutines(); //Se evita que se reactive la IA tras acabar el Stun si se esta poseyendo.
        this_EnemyAI.enabled = false;
        this_EnemyNavAgent.enabled = false;
        this_EnemyControl_MovementController.enabled = true;
        player_MovementController.controlSpeedX = 0;
        player_MovementController.controlSpeedY = 0;
        GameManager.Instance.realPlayerGO.SetActive(false);
        GameManager.Instance.ActualPlayerController = this_EnemyControl_MovementController;
    }

    private float lastSpeedX;
    private float lastSpeedY;

    public void UnpossessEnemy() 
    {
        this_EnemyControl_MovementController.enabled = false;  ////Se desactiva el control del enemigo, se activa el jugador y se indica que es el ActualPlayer; se le coloca en la posicion del enemigo y se eliminan las colisiones entre ambos
        GameManager.Instance.realPlayerGO.transform.position = transform.position;
        GameManager.Instance.realPlayerGO.SetActive(true);
        GameManager.Instance.ActualPlayerController = player_MovementController;
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameManager.Instance.realPlayerGO.GetComponent<Collider2D>(), true);

        lastSpeedX = this_EnemyControl_MovementController.controlSpeedX; ////Se guradan las velocidades de X e Y para realizar la fuerza en dirección contraria, y se igualan a 0 para evitar moviemiento residual al volver a poseer.
        lastSpeedY = this_EnemyControl_MovementController.controlSpeedY;
        this_EnemyControl_MovementController.controlSpeedX = 0;
        this_EnemyControl_MovementController.controlSpeedY = 0;
        thisEnemyRB.velocity = Vector2.zero;  ////Se elimina toda velocidad del RB y se aplica una fuerza contraria a la velocidad en la que se estaba moviendo en el último momento
        thisEnemyRB.AddForce(new Vector2(-lastSpeedX, -lastSpeedY) * 2, ForceMode2D.Impulse);

        StartCoroutine(Stun()); ////Se inicia el Stun.
    }

    public void ConsumeEnemy()
    {

    }

    public IEnumerator Stun()  //Se reactiva la IA y el agente al acabar el tiempo de Stun
    {
        Debug.Log("Stunned");

        yield return new WaitForSeconds(timeStunned);

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameManager.Instance.realPlayerGO.GetComponent<Collider2D>(), false);
        this_EnemyNavAgent.enabled = true;
        this_EnemyAI.enabled = true;
        Debug.Log("NOT Stunned");
    }
}
