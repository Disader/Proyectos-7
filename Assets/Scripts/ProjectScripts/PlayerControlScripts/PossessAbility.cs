﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessAbility : MonoBehaviour
{
    [Header("Variables del Raycast")]
    public float raycastDistance;
    private RaycastHit2D raycastHit;

    [Header("El Enemigo en el Raycast")]
    private EnemySetControl enemy_InRaycast;

    [Header("El PlayerControl del mismo objeto Player")]
    PlayerControl_MovementController playerControl_MovementController;

    [Header("El Objeto del Brazo, Sacado del Script de Control en Start")]
    private GameObject armObject;

    [Header("El LineRenderer")]
    private LineRenderer playerLineRenderer;

    [Header("Variables para usar los Triggers de Mando como Botón")]
    private bool leftTrigger_isAxisInUse;

    // Start is called before the first frame update
    void Start()
    {
        playerControl_MovementController = GetComponent<PlayerControl_MovementController>();
        armObject = playerControl_MovementController.armObject;
        playerLineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        LaunchRaycast();
        LeftTriggerInput();
    }

    void LaunchRaycast()
    {
        if (playerControl_MovementController.armDirection.sqrMagnitude > 0) ////Se lanza Raycast si hay input de rotación registrado en el PlayerControl
        {
            raycastHit = Physics2D.Raycast(armObject.transform.position, armObject.transform.right, raycastDistance); ////Actualmente el Raycast tiene siempre la misma distancia, sin intervenir el valor del joystick
            Debug.DrawRay(armObject.transform.position, armObject.transform.right * raycastDistance, Color.green);
            playerLineRenderer.SetPosition(1, playerControl_MovementController.armDirection * raycastDistance); ////Actualmente el punto final de LineRenderer se multiplica por el valor del joystick
        }

        else ////Si no hay input de rotación, se elimina la visibilidad de LineRenderer y se resetea el RaycastHit;
        {
            playerLineRenderer.SetPosition(1, Vector3.zero);
            raycastHit = new RaycastHit2D();
        }

        if (raycastHit.collider != null) ////Chequeos para vaciar la variable del EnemySetControl del enemigo en el Raycast si no hay ninguno en el Raycast o si se salta de uno a otro sin que no haya nada entre medias.
        {
            EnemySetControl enemyCheckScript = raycastHit.collider.gameObject.GetComponent<EnemySetControl>();

            if (enemyCheckScript != null)
            {
                if (enemy_InRaycast != null)
                {
                    enemy_InRaycast = null;
                }
                enemy_InRaycast = enemyCheckScript;
            }
        }

        else
        {
            if (enemy_InRaycast != null)
            {
                enemy_InRaycast = null;
            }
        }
    }

    void PossessAction()
    {
        if (enemy_InRaycast != null)
        {
            playerLineRenderer.SetPosition(1, Vector3.zero);
            enemy_InRaycast.PosssessEnemy();
        }
    }

    private void LeftTriggerInput()
    {
        if (Input.GetAxisRaw("LeftTrigger") != 0)
        {
            if (leftTrigger_isAxisInUse == false)
            {
                PossessAction();

                leftTrigger_isAxisInUse = true;
            }
        }
        else if (Input.GetAxisRaw("LeftTrigger") == 0)
        {
            leftTrigger_isAxisInUse = false;
        }
    }
}
