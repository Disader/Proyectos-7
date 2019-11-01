using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    //Esta función es WIP solo para probar que la carga asíncrona funciona
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.
        int c = SceneManager.sceneCount;
        for (int i = 0; i < c; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != "SaveRoom" && scene.name!="PermanentScene")
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Sergio_Test", LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
