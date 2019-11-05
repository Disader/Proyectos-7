using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.name, LoadSceneMode.Single);
    }
    public void  StartGame ()
    {
        SceneManager.LoadScene("LoadingScene", LoadSceneMode.Single);
    }
}
