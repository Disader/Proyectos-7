using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySetControl : MonoBehaviour
{
    [Header("El PlayerControl de este Enemigo")]
    private EnemyControl_MovementController this_EnemyControl_MovementController;

    [Header("El Script de Disparo/Aatque")]
    private ShootingScript this_EnemyShootingScript;

    [Header("La Pasiva del Enemigo")]
    private ActiveAbility this_EnemyActiveAbility;

    [Header("La IA y el Agente del Enemigo")]
    private EnemyAI_Standard this_EnemyAI;
    private NavMeshAgent this_EnemyNavAgent;

    [Header("El PlayerControl del Jugador")]
    private PlayerControl_MovementController player_MovementController;

    [Header("La Possess Ability del Jugador")]
    private PossessAbility player_PossessAbility;

    [Header("RigidBodies")]
    private Rigidbody2D thisEnemyRB;

    [Header("El Transform donde hace TP el Jugador al Desposeer/Consumir")]
    public Transform playerSpawnPos;

    [Header("Variables de Consumir")]
    public float timeToConsume;
    private bool hasBeenConsumed;
    [Header("Fragmentos de Vida Recuperados al ser Consumido")]
    public int healthHealedOnCosuming;

    [Header("Variables Stun del Enemigo")]
    public float timeStunned;

    [Header("Tiempo de Stun de Player si este Enemigo Muere Estando Poseído")]
    public float playerTimeStunned;

    [Header("Partículas (PlaceHolders)")]
    public GameObject deathPart;
    public ParticleSystem smokeThrowPart;
    public ParticleSystem stunPart;
    public ParticleSystem consumePart;

    [Header("Animator del enemigo")]
    [SerializeField] Animator characterAnimator; //Animator para cambiar si se está poseyendo o no

    [SerializeField] GameObject deathDummy;
    // Start is called before the first frame update
    void Start()
    {
        this_EnemyControl_MovementController = GetComponent<EnemyControl_MovementController>();
        this_EnemyShootingScript = GetComponent<ShootingScript>();
        this_EnemyActiveAbility = GetComponent<ActiveAbility>();
        this_EnemyAI = GetComponent<EnemyAI_Standard>();
        this_EnemyNavAgent = GetComponent<NavMeshAgent>();
        player_MovementController = GameManager.Instance.realPlayerGO.GetComponent<PlayerControl_MovementController>();
        player_PossessAbility = GameManager.Instance.realPlayerGO.GetComponent<PossessAbility>();
        thisEnemyRB = GetComponent<Rigidbody2D>();
    }

    Color originalColor; //PLACEHOLDER
    public void PosssessEnemy()  ////Se desactiva la IA y el agente, se activa el control de enemigo, se detiene el movimiento residual del control de jugador, se desactiva el objeto del jugador y se indica a GameManager que este enmigo es ActualPlayer
    {
        StopAllCoroutines(); //Se evita que se reactive la IA tras acabar el Stun si se esta poseyendo.

        if (stunPart.isPlaying) //PLACEHOLDER
        {
            stunPart.Stop();
        }

        if (smokeThrowPart.isPlaying) //PLACEHOLDER
        {
            smokeThrowPart.Stop();
        }

        this_EnemyAI.enabled = false;
        this_EnemyNavAgent.enabled = false;
        this_EnemyControl_MovementController.enabled = true;
        player_MovementController.controlSpeedX = 0;
        player_MovementController.controlSpeedY = 0;

        gameObject.layer = 8; ////Al ser poseído pasa a tener layer de Player

        if (this_EnemyActiveAbility != null)
        {
            this_EnemyActiveAbility.SetCurrentAbility(this_EnemyShootingScript); ////VER el método en ActiveAbility     !!!!!  
        }

        GameManager.Instance.realPlayerGO.SetActive(false);
        GameManager.Instance.ActualPlayerController = this_EnemyControl_MovementController;

        ///Sonido
        MusicManager.Instance.PlaySound(AppSounds.PLAYER_CONSUME); ///// PLACEHOLDER

        //Seteo de animaciones
        characterAnimator.SetBool("IsPossessed", true);

        //Seteo del color del sprite 
        originalColor = characterAnimator.gameObject.GetComponent<SpriteRenderer>().color; //PLACEHOLDER
        characterAnimator.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.36f, 0.36f, 0.36f, 1); //PLACEHOLDER
    }

    private float lastSpeedX;
    private float lastSpeedY;

    public void UnpossessEnemy() 
    {
        this_EnemyControl_MovementController.enabled = false;  ////Se desactiva el control del enemigo, se activa el jugador y se indica que es el ActualPlayer; se le coloca en la posicion del enemigo y se eliminan las colisiones entre ambos
        GameManager.Instance.realPlayerGO.transform.position = playerSpawnPos.position;
        GameManager.Instance.realPlayerGO.SetActive(true);
        GameManager.Instance.ActualPlayerController = player_MovementController;
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameManager.Instance.realPlayerGO.GetComponent<Collider2D>(), true);

        if(!smokeThrowPart.isPlaying) //PLACEHOLDER
        {
            smokeThrowPart.Play();
        }

        gameObject.layer = 9; ////Al ser desposeído vuelve a tener layer de Enemy

        if (this_EnemyActiveAbility != null)
        {
            this_EnemyActiveAbility.EraseCurrentAbility(this_EnemyShootingScript); //// VER el método en ActiveAbility      !!!!!
        }

        lastSpeedX = this_EnemyControl_MovementController.controlSpeedX; ////Se guradan las velocidades de X e Y para realizar la fuerza en dirección contraria, y se igualan a 0 para evitar moviemiento residual al volver a poseer.
        lastSpeedY = this_EnemyControl_MovementController.controlSpeedY;
        this_EnemyControl_MovementController.controlSpeedX = 0;
        this_EnemyControl_MovementController.controlSpeedY = 0;

        thisEnemyRB.velocity = Vector2.zero;  ////Se elimina toda velocidad del RB y se aplica una fuerza contraria a la velocidad en la que se estaba moviendo en el último momento
        thisEnemyRB.AddForce(new Vector2(-lastSpeedX, -lastSpeedY) * 2, ForceMode2D.Impulse);

        StartCoroutine(StunEnemy()); ////Se inicia el Stun.

        //Seteo de animaciones
        characterAnimator.SetBool("IsPossessed", false);
        //Seteo del color del sprite 
        characterAnimator.gameObject.GetComponent<SpriteRenderer>().color = originalColor; //PLACEHOLDER
    }

    public void CheckEnemyDeath() ////Comprueba si el enemigo ha muerto poseído o no y actúa en consecuencia
    {
       
        if (this_EnemyAI.enabled == false) ////El enemigo está poseído
        {
            EnemyDeadWhilePossessed();
        }

        else if (this_EnemyAI.enabled == true)//// El enemigo no está poseído
        {
            Instantiate(deathPart, transform.position, transform.rotation); //PLACEHOLDER
            Instantiate(deathDummy, transform.position, transform.rotation);//PLACEHOLDER Instanciar dummy
            gameObject.SetActive(false);
        }

        ///Sonido
        int random = Random.Range(0, 3);
        switch (random)
        {
            case 0:
                MusicManager.Instance.PlaySound(AppSounds.ENEMY_DEATH1); ///// PLACEHOLDER
                break;
            case 1:
                MusicManager.Instance.PlaySound(AppSounds.ENEMY_DEATH2); ///// PLACEHOLDER
                break;
            case 2:
                MusicManager.Instance.PlaySound(AppSounds.ENEMY_DEATH3); ///// PLACEHOLDER
                break;
            default:
                break;
        }
       


        ZoneManager.Instance.DeleteEnemyFromCurrentRoom(this_EnemyControl_MovementController);
    }

    public void EnemyDeadWhilePossessed() ////Funcionalidad al morir el enemigo mientras estaba poseído
    {
        StopAllCoroutines();

        GameManager.Instance.realPlayerGO.transform.position = playerSpawnPos.position;
        GameManager.Instance.realPlayerGO.SetActive(true);
        GameManager.Instance.ActualPlayerController = player_MovementController;

        this_EnemyControl_MovementController.controlSpeedX = 0; ////Eliminar movimiento residual y desactivar control de enemigo
        this_EnemyControl_MovementController.controlSpeedY = 0;
        this_EnemyControl_MovementController.enabled = false;

        if (this_EnemyActiveAbility != null)
        {
            this_EnemyActiveAbility.EraseCurrentAbility(this_EnemyShootingScript); ////Eliminar la Pasiva Activa si hay
        }

        if (!hasBeenConsumed) ///Si el enemigo ha muerto poseído pero no por ser consumido, es decir, muerto por ataque, se aplica stun al jugador. Esta funcionalidad está en PossessAbility
        {
            player_PossessAbility.StartCoroutine(player_PossessAbility.PlayerStun(playerTimeStunned));
            Instantiate(deathDummy, transform.position, transform.rotation);//PLACEHOLDER Instanciar dummy
        }

        gameObject.layer = 9; //Layer de enemy
        hasBeenConsumed = false; //Se resetea el bool que indica si el enemigo ha sido consumido.
        Instantiate(deathPart, transform.position, transform.rotation); //PLACEHOLDER
        gameObject.SetActive(false);
    }

    public IEnumerator ConsumeEnemy()  ////NO POLISHEADO; Faltan animaciones y su relación
    {
        thisEnemyRB.velocity = Vector2.zero;
        this_EnemyControl_MovementController.enabled = false;
        consumePart.Play(); //Partículas de consumir PLACEHOLDER

        ///Sonido
        MusicManager.Instance.PlaySound(AppSounds.PLAYER_CONSUME); ///// PLACEHOLDER


        yield return new WaitForSeconds(timeToConsume);

        if (this_EnemyActiveAbility != null)
        {
            this_EnemyActiveAbility.SaveAbility();
        }
        consumePart.Stop(); //Partículas de consumir PLACEHOLDER
        HealthHeartsVisual.healthHeartsSystemStatic.Heal(healthHealedOnCosuming);
        hasBeenConsumed = true;
        CheckEnemyDeath();
    }

    public IEnumerator StunEnemy()  //Se reactiva la IA y el agente al acabar el tiempo de Stun
    {
        if(!stunPart.isPlaying) //PLACEHOLDER
        {
            stunPart.Play();
        }

        yield return new WaitForSeconds(timeStunned);

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameManager.Instance.realPlayerGO.GetComponent<Collider2D>(), false);
        this_EnemyNavAgent.enabled = true;
        this_EnemyAI.enabled = true;

        if(stunPart.isPlaying) //PLACEHOLDER
        {
            stunPart.Stop();
        }

        if(smokeThrowPart.isPlaying) //PLACEHOLDER
        {
            smokeThrowPart.Stop(); 
        }

    }

    private void OnDisable() ////Para cuando se reactiva un enemigo que estaba en Stun;
    {
        if(this_EnemyAI != null && this_EnemyNavAgent != null)
        {
            this_EnemyAI.enabled = true;
            this_EnemyNavAgent.enabled = true;
        }
    }
}
