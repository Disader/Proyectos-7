using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : TemporalSingleton<GameManager>
{
    PlayerControl_MovementController m_actualPlayerController;
    [HideInInspector]public GameObject realPlayerGO;
    public static bool isGamePaused;

    //Se llama a esta variable en el onEnable de playercontrolMovementController
   public PlayerControl_MovementController ActualPlayerController
    {
        get {return m_actualPlayerController; }
        set { m_actualPlayerController = value; }
    }

    public override void Awake()
    {
        base.Awake();
        realPlayerGO = GameObject.FindGameObjectWithTag("Player");
        ActualPlayerController = realPlayerGO.GetComponent<PlayerControl_MovementController>();
    }
    public void PauseGame()
    {
        if (isGamePaused) 
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
  
    }
}
