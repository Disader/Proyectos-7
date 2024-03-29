﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    //Esta función es WIP solo para probar que la carga asíncrona funciona
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent <PlayerControl_MovementController>() != null)
        {
            StartCoroutine(LoadYourAsyncScene());

            GameManager.Instance.SaveGame(transform.position); ////Guardar el juego al llegar a Checkpoint
            HealthHeartsVisual.healthHeartsSystemStatic.Heal(100);
        }
    }
    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        SceneManager.UnloadSceneAsync(ZoneManager.Instance.zoneScene);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(ZoneManager.Instance.zoneScene, LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

       if (asyncLoad.isDone)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);

                if(!scene.name.Contains("Permanent") && !scene.name.Contains("Save"))
                {
                    SceneManager.SetActiveScene(scene);
                }
            }
        }
    }
}
