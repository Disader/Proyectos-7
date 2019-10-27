using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySetControl : MonoBehaviour
{
    [Header("El PlayerControl de este Enemigo")]
    private EnemyControl_MovementController this_EnemyControl_MovementController;

    [Header("El PlayerControl del Jugador")]
    private PlayerControl_MovementController player_MovementController;

    // Start is called before the first frame update
    void Start()
    {
        this_EnemyControl_MovementController = GetComponent<EnemyControl_MovementController>();
        player_MovementController = GameManager.Instance.ActualPlayerController.gameObject.GetComponent<PlayerControl_MovementController>();
    }

    public void PosssessEnemy()  ////Se activa el control de enemigo, se detiene el movimiento residual del control de jugador y se desactiva el objeto del jugador
    {
        this_EnemyControl_MovementController.enabled = true;
        player_MovementController.controlSpeedX = 0;
        player_MovementController.controlSpeedY = 0;
        GameManager.Instance.realPlayerGO.SetActive(false);
    }

    public void UnpossessEnemy() ////Se detiene el movimiento residual del control del enemigo poseído, se desactiva el contro, y se activa de nuevo el jugador
    {
        this_EnemyControl_MovementController.controlSpeedX = 0;
        this_EnemyControl_MovementController.controlSpeedY = 0;
        //gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;  ////TEMPORAL
        this_EnemyControl_MovementController.enabled = false;
        GameManager.Instance.realPlayerGO.SetActive(true);
    }

    public void ConsumeEnemy()
    {

    }
}
