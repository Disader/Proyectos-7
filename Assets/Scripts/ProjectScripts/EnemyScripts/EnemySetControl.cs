using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySetControl : MonoBehaviour
{
    [Header("El PlayerControl de este Enemigo")]
    private PlayerControl_MovementController this_EnemyPlayerControl_MovementController;


    // Start is called before the first frame update
    void Start()
    {
        this_EnemyPlayerControl_MovementController = GetComponent<PlayerControl_MovementController>();
    }

    public void PosssessEnemy(PlayerControl_MovementController playerController)
    {
        Debug.Log("POSS");
        this_EnemyPlayerControl_MovementController.enabled = true;
        playerController.enabled = false;
    }

    void UnpossessEnemy()
    {

    }

    void ConsumeEnemy()
    {

    }
}
