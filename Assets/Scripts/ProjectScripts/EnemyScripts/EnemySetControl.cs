﻿using System.Collections;
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

    [Header("El Objeto de pies del enemigo")]
    public GameObject enemyFeet;

    [Header("El Transform donde hace TP el Jugador al Desposeer/Consumir")]
    public Transform playerSpawnPos;

    [Header("Variables de Consumir")]
    public float timeToConsume;
    private float calculatedTimeToConsume;
    private bool hasBeenConsumed;
    [Header("El Script de Vida del Enemigo")]
    private EnemyHealth this_EnemyHealthScript;
    [Header("Fragmentos de Vida Recuperados al ser Consumido")]
    public int healthHealedOnCosuming;

    [Header("Variables de desposesión del Enemigo")]
    public float timeStunned;
    [SerializeField] float m_despossessForceMangitude=10;

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
    protected virtual void Start()
    {
        StartSaveVariables();
    }
    void StartSaveVariables()
    {
        this_EnemyControl_MovementController = GetComponent<EnemyControl_MovementController>();
        this_EnemyShootingScript = GetComponent<ShootingScript>();
        this_EnemyActiveAbility = GetComponent<ActiveAbility>();
        this_EnemyAI = GetComponent<EnemyAI_Standard>();
        this_EnemyNavAgent = GetComponent<NavMeshAgent>();
        this_EnemyHealthScript = GetComponent<EnemyHealth>();
        player_MovementController = GameManager.Instance.realPlayerGO.GetComponent<PlayerControl_MovementController>();
        player_PossessAbility = GameManager.Instance.realPlayerGO.GetComponent<PossessAbility>();
        thisEnemyRB = GetComponent<Rigidbody2D>();
    }

    Color originalColor; //PLACEHOLDER
    [HideInInspector] public bool isBeingPossessed;
    public void PossessEnemy()  ////Se desactiva la IA y el agente, se activa el control de enemigo, se detiene el movimiento residual del control de jugador, se desactiva el objeto del jugador y se indica a GameManager que este enmigo es ActualPlayer
    {
        isBeingPossessed = true;
        StopAllCoroutines(); //Se evita que se reactive la IA tras acabar el Stun si se esta poseyendo.
        StopParticles();

        PossessEnemySetComponents();

        //Frenar al jugador real, para evitar que se mueva al volver a usarlo
        player_MovementController.controlSpeedX = 0;
        player_MovementController.controlSpeedY = 0;

        gameObject.layer = 8; ////Al ser poseído pasa a tener layer de Player

        if (this_EnemyActiveAbility != null)
        {
            this_EnemyActiveAbility.SetCurrentAbility(this_EnemyShootingScript); ////VER el método en ActiveAbility     !!!!!  
        }

        PossessSetPlayersInGameManager();

        PossessEnemyEffects();
    }

    void PossessEnemySetComponents()
    {
        this_EnemyAI.enabled = false;
        this_EnemyNavAgent.enabled = false;
        this_EnemyControl_MovementController.enabled = true;
    }

    void PossessEnemyEffects()
    {
        ///Sonido
        MusicManager.Instance.PlaySound(AppSounds.PLAYER_CONSUME); ///// PLACEHOLDER

        //Seteo de animaciones
        characterAnimator.SetBool("IsPossessed", true);

        //Seteo del color del sprite 
        originalColor = characterAnimator.gameObject.GetComponent<SpriteRenderer>().color; //PLACEHOLDER
        characterAnimator.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.36f, 0.36f, 0.36f, 1); //PLACEHOLDER
    }

    void PossessSetPlayersInGameManager()
    {
        GameManager.Instance.realPlayerGO.GetComponent<PlayerHealthController>().ResetPlayerStates();////////      Elimina los estados alterados del jugador al comenzar a poseerlo
                                                                                                         //// El orden es importante para que se resetee antes de desactivarlo
        GameManager.Instance.realPlayerGO.SetActive(false);                                          ////////      

        GameManager.Instance.ActualPlayerController = this_EnemyControl_MovementController;
    }

    private float lastSpeedX;
    private float lastSpeedY;
    public void UnpossessEnemy()  
    {
        isBeingPossessed = false;
        ////Se desactiva el control del enemigo, se activa el jugador y se indica que es el ActualPlayer; se le coloca en la posicion del enemigo y se eliminan las colisiones entre ambos
        UnpossessEnemySetComponents(); 

        UnpossessSetPlayersInGameManager();

        if (!smokeThrowPart.isPlaying) //PLACEHOLDER
        {
            smokeThrowPart.Play();
        }

        gameObject.layer = 9; ////Al ser desposeído vuelve a tener layer de Enemy

        if (this_EnemyActiveAbility != null)
        {
            this_EnemyActiveAbility.EraseCurrentAbility(this_EnemyShootingScript); //// VER el método en ActiveAbility      !!!!!
        }

        
        this_EnemyControl_MovementController.controlSpeedX = 0;
        this_EnemyControl_MovementController.controlSpeedY = 0;
        thisEnemyRB.velocity = Vector2.zero;  ////Se elimina toda velocidad del RB y se aplica una fuerza contraria a la velocidad en la que se estaba moviendo en el último momento

        thisEnemyRB.AddForce(InputManager.Instance.RotationInput()* m_despossessForceMangitude, ForceMode2D.Impulse);

        StartCoroutine(StunEnemy()); ////Se inicia el Stun.

        UnpossessEnemyEffects();
    }

    void UnpossessEnemySetComponents()
    {
        this_EnemyControl_MovementController.enabled = false;
    }

    void UnpossessEnemyEffects()
    {

        //Seteo de animaciones
        characterAnimator.SetBool("IsPossessed", false);
        //Seteo del color del sprite 
        characterAnimator.gameObject.GetComponent<SpriteRenderer>().color = originalColor; //PLACEHOLDER
    }

    void UnpossessSetPlayersInGameManager()
    {
        GameManager.Instance.realPlayerGO.transform.position = playerSpawnPos.position;
        GameManager.Instance.realPlayerGO.SetActive(true);
        GameManager.Instance.ActualPlayerController = player_MovementController;
        Physics2D.IgnoreCollision(enemyFeet.GetComponent<Collider2D>(), GameManager.Instance.realPlayerGO.GetComponent<Collider2D>(), true);
    }

    public virtual void CheckEnemyDeath() ////Comprueba si el enemigo ha muerto poseído o no y actúa en consecuencia
    {  
        if (isBeingPossessed) ////El enemigo está poseído
        {
            EnemyDeadWhilePossessed();
        }

        else if (!isBeingPossessed)//// El enemigo no está poseído
        {
            Instantiate(deathPart, transform.position, transform.rotation); //PLACEHOLDER
            ZoneManager.Instance.m_activeRoom.AddDummieAtRoom(Instantiate(deathDummy, transform.position, transform.rotation));  //PLACEHOLDER Instanciar dummy
            gameObject.SetActive(false);
        }
       
        if(this_EnemyControl_MovementController.spawnerInstantiatedFrom != null)
        {
            this_EnemyControl_MovementController.spawnerInstantiatedFrom.RemoveEnemyFromSpawner(this.gameObject);
        }
        ZoneManager.Instance.DeleteEnemyFromCurrentRoom(this_EnemyControl_MovementController);

        PlayEnemyDeathSound();
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
            ZoneManager.Instance.m_activeRoom.AddDummieAtRoom(Instantiate(deathDummy, transform.position, transform.rotation));  //PLACEHOLDER Instanciar dummy
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

        if (this_EnemyHealthScript.enemyHealth >= (float)this_EnemyHealthScript.defaultEnemyHealth / 2)
        {
            //Si la vida del enemigo está por encima de la mitad, al tiempo de consumir se le añade un máximo del doble del tiempo base en función de la vida actual del enemigo
            calculatedTimeToConsume = timeToConsume + (timeToConsume * ((float)this_EnemyHealthScript.enemyHealth / this_EnemyHealthScript.defaultEnemyHealth));
        }

        else
        {
            //Si está por debajo de la mitad, se aplica el tiempo normal.
            calculatedTimeToConsume = timeToConsume;
        }

        yield return new WaitForSeconds(calculatedTimeToConsume);   //Se aplica el tiempo calculado

        if (this_EnemyActiveAbility != null)
        {
            this_EnemyActiveAbility.SaveAbility();
        }
        
        consumePart.Stop(); //Partículas de consumir PLACEHOLDER
        HealthHeartsVisual.healthHeartsSystemStatic.Heal(healthHealedOnCosuming);
        hasBeenConsumed = true;
        CheckEnemyDeath();
    }

    Coroutine consumingCoroutine;
    public void StartConsuming()
    {
        consumingCoroutine = StartCoroutine(ConsumeEnemy());
    }

    public void StopConsumingAction()
    {
        if (consumingCoroutine != null)
        {
            StopCoroutine(consumingCoroutine);
            this_EnemyControl_MovementController.enabled = true;
            consumePart.Stop();
        }        
    }

    public virtual IEnumerator StunEnemy()  //Se reactiva la IA y el agente al acabar el tiempo de Stun
    {
        if(!stunPart.isPlaying) //PLACEHOLDER
        {
            stunPart.Play();
        }

        yield return new WaitForSeconds(timeStunned);

        Physics2D.IgnoreCollision(enemyFeet.GetComponent<Collider2D>(), GameManager.Instance.realPlayerGO.GetComponent<Collider2D>(), false);
        this_EnemyNavAgent.enabled = true;
        this_EnemyAI.enabled = true;

        StopParticles();

    }

    void PlayEnemyDeathSound()
    {
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
    }
    void StopParticles()
    {
        if (stunPart.isPlaying) //PLACEHOLDER
        {
            stunPart.Stop();
        }

        if (smokeThrowPart.isPlaying) //PLACEHOLDER
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
