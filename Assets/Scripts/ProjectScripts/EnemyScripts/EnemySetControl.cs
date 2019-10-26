using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySetControl : MonoBehaviour
{
    [Header("El PlayerControl de este Enemigo")]
    private PlayerControl_MovementController this_EnemyPlayerControl_MovementController;

    [Header("El PlayerControl del Jugador")]
    private PlayerControl_MovementController player_MovementController;

    // Start is called before the first frame update
    void Start()
    {
        this_EnemyPlayerControl_MovementController = GetComponent<PlayerControl_MovementController>();
        player_MovementController = GameManager.Instance.ActualPlayerController.gameObject.GetComponent<PlayerControl_MovementController>();
    }

    public void PosssessEnemy()  ////Se activa el control de enemigo, se detiene el movimiento residual del control de jugador y se desactiva el objeto del jugador
    {
        this_EnemyPlayerControl_MovementController.enabled = true;
        player_MovementController.playerSpeedX = 0;
        player_MovementController.playerSpeedY = 0;
        GameManager.Instance.ActualPlayerController.gameObject.SetActive(false);
    }

    public void UnpossessEnemy() ////Se detiene el movimiento residual del control del enemigo poseído, se desactiva el contro, y se activa de nuevo el jugador
    {
        this_EnemyPlayerControl_MovementController.playerSpeedX = 0;
        this_EnemyPlayerControl_MovementController.playerSpeedY = 0;
        this_EnemyPlayerControl_MovementController.enabled = false;
        GameManager.Instance.ActualPlayerController.gameObject.SetActive(true);
    }

    public void ConsumeEnemy()
    {

    }
}
