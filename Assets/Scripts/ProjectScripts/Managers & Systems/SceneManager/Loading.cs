using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public IEnumerator loadDelay(SceneReference gameScene, SceneReference permanentScene, SceneReference saveScene)
    {
        yield return new WaitForEndOfFrame();
        SceneManager.LoadSceneAsync(gameScene, LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync(permanentScene, LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync(saveScene, LoadSceneMode.Additive);
        SceneManager.sceneLoaded += FinishLoading;
    }

    void FinishLoading(Scene scene, LoadSceneMode mode)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene sceneCheck = SceneManager.GetSceneAt(i);

            if (!sceneCheck.name.Contains("Permanent") && !sceneCheck.name.Contains("Save"))
            {
                SceneManager.SetActiveScene(sceneCheck);
            }
        }
        SceneManager.sceneLoaded -= FinishLoading;
        SceneManager.UnloadSceneAsync("LoadingScene");
        Destroy(this.gameObject);
    }
}
