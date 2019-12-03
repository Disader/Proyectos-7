using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnemyControl_MovementController : PlayerControl_MovementController  ///////////HEREDA DE PLAYERCONTROL_MOVEMENTCONTROLLER////////////
{
    [Header("El EnemySetControl de este enemigo")]
    EnemySetControl thisEnemySetControl;

    [Header("El script de Disparo")]
    ShootingScript thisEnemyShootingScript; 

    [Header("Variables para usar los Triggers de Mando como Botón")]
    private bool leftTrigger_isAxisInUse;
    private bool canUseLeftTrigger;
    [Header("Tiempo que tiene que estar mantenido el LeftTrigger para que cuente como Hold")]
    public float leftTriggerHoldTime;
    private float leftTriggerTimer;

    [Header("Indicar SI PUEDE O NO hacer Dash (Tick -> SI)")]
    public bool ableToDash;

    [Header("Variables del Dash")]
    public float dashTime;
    public float dashForce;
    private Vector2 dashDirection = new Vector2(1, 0);
    private bool isDashing = false;

    [HideInInspector] public Spawner spawnerInstantiatedFrom;
    void OnEnable()
    {
        canUseLeftTrigger = false;  ////Este bool impide que al poseer al enemigo con el left trigger, se inicie inmediatamente el Input de LeftTrigger que desposee al enemigo en este script
    }

    protected override void Start()
    {
        base.Start();
        m_minimaparrowAnimator = UIManager.minimapArrow;
        thisEnemySetControl = GetComponent<EnemySetControl>();
        thisEnemyShootingScript = GetComponent<ShootingScript>();
    }

    protected override void Update()
    {
        if (!isDashing) ////Al hacer Dash se quita el control al jugador
        {
            base.Update();

            LeftTriggerInput();
            RightTriggerInput();

            CheckDashDirection();

            
            if (InputManager.Instance.DashButtonTriggered()) ////Input de Dash en la "B" CAMBIAR SI NECESARIO
            {
                if (ableToDash)
                {
                    StartCoroutine(DashLogicCoroutine());
                }
            }           
        }
    }

    /// <summary>
    /// /////////////////// DASHING /////////////////////////  HAY QUE REPLANTEARLO CON ANIMACIONES SI ES NECESARIO
    /// </summary>

    private void CheckDashDirection() ////Se guarda la última dirección de movimiento como la dirección de Dash si hay Input
    {
        if (InputManager.Instance.HorizontalMovement() != 0 || InputManager.Instance.VerticalMovement() != 0)
        {
            dashDirection = new Vector2(controlSpeedX, controlSpeedY);
        }
    }

    private IEnumerator DashLogicCoroutine() ////Se aplica el Dash
    {
        isDashing = true;

        controlRb.velocity = dashDirection.normalized * dashForce;

        ////ANIMACIONES////
        m_characterAnimator.SetTrigger("Dodge");
        ///FINAL ANIMACIONES/////


        yield return new WaitForSeconds(dashTime);

        controlRb.velocity = Vector2.zero;

        isDashing = false;
    }

    /// <summary>
    /// ////////////////////////////////////////////////////////
    /// </summary>

    private void UnpossessAction()
    {
        thisEnemySetControl.UnpossessEnemy();
    }

    private void ConsumeAction()
    {
        thisEnemySetControl.StartConsuming();
    }
   
    private void AttackAction() 
    {
        if (thisEnemyShootingScript!=null)
        {
            thisEnemyShootingScript.FireInShootingPos(ShootingScript.whoIsShooting.player);
        }
        
    }

    private void CallShootingScriptReset() ////Llama al Reset de disparo de ShootingScript. VER el método en Shooting Script       !!!!
    {
        thisEnemyShootingScript.ResetPlayerFiringRateTimer();
    }

    private void LeftTriggerInput()
    {
        if (InputManager.Instance.LeftTrigger() != 0)
        {
            if (canUseLeftTrigger)
            {
                leftTriggerTimer += 1 * Time.deltaTime;
                leftTrigger_isAxisInUse = true;

                if (leftTriggerTimer >= leftTriggerHoldTime)
                {
                    ConsumeAction();
                }
            }
        }
        else if (InputManager.Instance.LeftTrigger() == 0)
        {
            if(leftTrigger_isAxisInUse)
            {
                if(leftTriggerTimer < leftTriggerHoldTime)
                {
                    UnpossessAction();
                }
            }

            leftTriggerTimer = 0;
            canUseLeftTrigger = true;
            leftTrigger_isAxisInUse = false;
        }
    }

    private void RightTriggerInput()
    {
        if (InputManager.Instance.RightTrigger() != 0)
        {
            if (thisEnemyShootingScript != null)
            {
                AttackAction();
            }
        }
        else if (InputManager.Instance.RightTrigger() == 0)
        {
            if (thisEnemyShootingScript != null)
            {
                CallShootingScriptReset(); //Se llama al reset al dejar de pulsar Trigger
            }
        }
    }

    /////////////////////////////// ANIMACIONES///////////////////////////////////////////////

    protected override void SetAnimationsVariables()//Utilizado mientras testeo para evitar que se rompa mientras no están implementadas las animaciones del enemigos
    {
        m_characterAnimator.SetBool("IsMoving", controlRb.velocity.magnitude > 0.1f || controlRb.velocity.magnitude < -0.1f);
        if (angle < 0)
        {
            angle = 360 + angle;
        }
        m_characterAnimator.SetFloat("Angle", angle);
        m_minimaparrowAnimator.SetFloat("VelocityY", controlRb.velocity.normalized.y);
        m_minimaparrowAnimator.SetFloat("VelocityX", controlRb.velocity.normalized.x);
    }
}
