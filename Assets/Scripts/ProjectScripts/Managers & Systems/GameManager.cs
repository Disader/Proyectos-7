using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : TemporalSingleton<GameManager>
{
    PlayerControl_MovementController m_actualPlayerController;
    [HideInInspector]public GameObject realPlayerGO;
    bool isGamePaused;

    //Se llama a esta variable en el onEnable de playercontrolMovementController
    public PlayerControl_MovementController ActualPlayerController
    {
        get {return m_actualPlayerController; }
        set { m_actualPlayerController = value; }
    }

    public override void Awake()
    {
        base.Awake();

        SaveSystem.Init();  ////Se inicia SaveSystem

        realPlayerGO = GameObject.FindGameObjectWithTag("Player");
        ActualPlayerController = realPlayerGO.GetComponent<PlayerControl_MovementController>();

        SaveGame(realPlayerGO.transform.position); ////Se guarda la posición del jugador al comenzar el juego
    }

    public void PauseGame()
    {
        if (!isGamePaused) 
        {
            isGamePaused = true;
            Time.timeScale = 0;
        }
        else
        {
            isGamePaused = false;
            Time.timeScale = 1;
        }
  
    }

    public void RespawnPlayer() ////Respawn de jugador
    {
        LoadGame();
        HealthHeartsVisual.healthHeartsSystemStatic.Heal(100);
    }

    /*private void Update() //// TEST DE GUARDADO
    {
        if (Input.GetKeyDown(KeyCode.Q))
            SaveGame(Vector3.zero);

        if (Input.GetKeyDown(KeyCode.E))
            LoadGame();
    }*/

    ///                                                         ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
    ///////////////// GUARDADO DE PARTIDA //////////////////////Se puede modificar para tener métodos que guarden o carguen cosas específicas y no todo de manera general, pero hay que cargar antes el archivo guardado para no hacer un NEW SaveObject y sobreescribir el contenido guardado/////////////////
    ///                                                         ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

    public void SaveGame(Vector3 savedPosition) ////Guardado de Datos, se pasa la posicion del checkpoint porque cada uno debe decir cual es su posición al guardar al entrar en su trigger
    {
        SaveObject savedObject = new SaveObject ////Aquí se igualan los datos del SaveObject a los datos que se quieren guardar
        {
            lastCheckPointPosition = savedPosition,
        };

        SaveSystem.Serialize(savedObject); ////Llamada al guardado en SaveSystem

        Debug.Log("Game Saved!");
    }

    public void LoadGame() ////Carga de Datos
    {
        SaveObject loadedObject = SaveSystem.Deserialize<SaveObject>(); ////LLamada de carga al SaveSystem

        if (loadedObject != null) ////Si hay datos, hay que igualar las variables que queremos con ese valor a las guardadas en el SaveObject
        {
            realPlayerGO.transform.position = loadedObject.lastCheckPointPosition;
            Debug.Log("Game Loaded!");
        }

        else    ////Si no hay ARCHIVO.XML con datos guardados, se idica
        {
            Debug.Log("No Save File!");
        }
    }
    

    public class SaveObject //// Clase que contiene la información de Guardado, todo lo que se quiera guardar tiene que tener una variable AQUI dentro del mismo tipo para que se pueda guardar
    {
        public Vector3 lastCheckPointPosition;
    }
}