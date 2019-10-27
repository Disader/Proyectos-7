﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl_MovementController : PlayerControl_MovementController  ///////////HEREDA DE PLAYERCONTROL_MOVEMENTCONTROLLER//////////
{
    [Header("El EnemySetControl de este enemigo")]
    EnemySetControl thisEnemySetControl;

    [Header("Variables para usar los Triggers de Mando como Botón")]
    private bool leftTrigger_isAxisInUse;
    private bool canUseLeftTrigger;

    private void OnEnable()
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
    }

    private void UnpossessAction()
    {
        thisEnemySetControl.UnpossessEnemy();
    }

    private void LeftTriggerInput()
    {
        if (Input.GetAxisRaw("LeftTrigger") != 0)
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
        else if (Input.GetAxisRaw("LeftTrigger") == 0)
        {
            canUseLeftTrigger = true;
            leftTrigger_isAxisInUse = false;
        }
    }
}
