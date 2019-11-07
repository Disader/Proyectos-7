using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    // Use this for initialization
    void OnEnable()
    {
        StartCoroutine(delay());
    }

    IEnumerator delay()
    {
        yield return new WaitForEndOfFrame();
        SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("SaveRoom", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("PermanentScene", LoadSceneMode.Additive);
        SceneManager.sceneLoaded += FinishLoading;
    }

    void FinishLoading(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= FinishLoading;
        Destroy(this.gameObject);
    }
}
