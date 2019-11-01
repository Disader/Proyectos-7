using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl_MovementController : PlayerControl_MovementController  ///////////HEREDA DE PLAYERCONTROL_MOVEMENTCONTROLLER////////////
{
    [Header("El EnemySetControl de este enemigo")]
    EnemySetControl thisEnemySetControl;

    [Header("El script de Disparo")]
    ShootingScript thisEnemyShootingScript; 

    [Header("Variables para usar los Triggers de Mando como Botón")]
    private bool leftTrigger_isAxisInUse;
    private bool canUseLeftTrigger;
    private bool rightTrigger_isAxisInUse;

    void OnEnable()
    {
        canUseLeftTrigger = false;  ////Este bool impide que al poseer al enemigo con el left trigger, se inicie inmediatamente el Input de LeftTrigger que desposee al enemigo en este script
    }

    protected override void Start()
    {
        base.Start();

        thisEnemySetControl = GetComponent<EnemySetControl>();
        thisEnemyShootingScript = GetComponent<ShootingScript>();
    }

    protected override void Update()
    {
        base.Update();

        LeftTriggerInput();
        RightTriggerInput();

        if(actions.PlayerInputActions.ActionButton.triggered) ////Input de Consumir en la "A" CAMBIAR SI NECESARIO
        {
            ConsumeAction();
        }
    }

    private void UnpossessAction()
    {
        thisEnemySetControl.UnpossessEnemy();
    }

    private void ConsumeAction()
    {
        thisEnemySetControl.StartCoroutine(thisEnemySetControl.ConsumeEnemy());
    }

    private void Attack() 
    {
        thisEnemyShootingScript.FireInShootingPos(ShootingScript.whoIsShooting.player);
    }

    private void CallShootingScriptReset() ////Llama al Reset de ShootingScript. VER el método en Shhoting Script       !!!!
    {
        thisEnemyShootingScript.ResetOnStopAttack();
    }

    private void LeftTriggerInput()
    {
        if (actions.PlayerInputActions.LeftTrigger.ReadValue<float>() != 0)
        {
            if (canUseLeftTrigger)
            {
                if (leftTrigger_isAxisInUse == false)
                {
                    UnpossessAction();

                    leftTrigger_isAxisInUse = true;
                }
            }
        }
        else if (actions.PlayerInputActions.LeftTrigger.ReadValue<float>() == 0)
        {
            canUseLeftTrigger = true;
            leftTrigger_isAxisInUse = false;
        }
    }

    private void RightTriggerInput()
    {
        if (actions.PlayerInputActions.RightTrigger.ReadValue<float>() != 0)
        {
            Attack();

            rightTrigger_isAxisInUse = true;
        }
        else if (actions.PlayerInputActions.RightTrigger.ReadValue<float>() == 0)
        {
            CallShootingScriptReset(); //Se llama al reset al dejar de pulsar Trigger
            rightTrigger_isAxisInUse = false;
        }
    }
}
