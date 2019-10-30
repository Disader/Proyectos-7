using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl_MovementController : PlayerControl_MovementController  ///////////HEREDA DE PLAYERCONTROL_MOVEMENTCONTROLLER//////////
{
    [Header("El EnemySetControl de este enemigo")]
    EnemySetControl thisEnemySetControl;

    [Header("Variables para usar los Triggers de Mando como Botón")]
    private bool leftTrigger_isAxisInUse;
    private bool canUseLeftTrigger;

    void OnEnable()
    {
        canUseLeftTrigger = false;  ////Este bool impide que al poseer al enemigo con el left trigger, se inicie inmediatamente el Input de LeftTrigger que desposee al enemigo en este script
    }

    protected override void Start()
    {
        base.Start();

        thisEnemySetControl = GetComponent<EnemySetControl>();
    }

    protected override void Update()
    {
        base.Update();

        LeftTriggerInput();

        if(actions.PlayerInputActions.ActionButton.triggered) ////Input de Consumir en la A CAMBIAR SI NECESARIO
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
        thisEnemySetControl.ConsumeEnemy();
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
}
