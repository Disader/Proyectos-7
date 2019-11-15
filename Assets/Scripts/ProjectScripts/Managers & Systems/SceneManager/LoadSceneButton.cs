using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    [Header("Las Escenas a cargar si se hace click en este Botón")]
    public SceneReference gameSceneToLoad;
    public SceneReference permanentSceneToLoad;
    public SceneReference saveSceneToLoad;

    [Header("La escena del Menú Principal")]
    private Scene thisScene;

    [Header("El Componente de Botón de este botón")]
    private Button thisButton;

    // Start is called before the first frame update
    void Start()
    {
        thisScene = SceneManager.GetActiveScene();
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(PassSceneReferences);
    }

    void PassSceneReferences()
    {
        StartCoroutine(PassSceneReferencesCoroutine());
    }

    IEnumerator PassSceneReferencesCoroutine()
    {
        SceneManager.LoadScene("LoadingScene", LoadSceneMode.Additive);

        yield return new WaitForSeconds(0.1f);

        Loading loadingSceneScript = FindObjectOfType<Loading>();
        loadingSceneScript.StartCoroutine(loadingSceneScript.loadDelay(gameSceneToLoad, permanentSceneToLoad, saveSceneToLoad));
        SceneManager.UnloadSceneAsync(thisScene);
    }
}
