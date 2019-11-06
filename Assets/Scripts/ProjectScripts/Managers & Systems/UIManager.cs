using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : TemporalSingleton<UIManager>
{
    // Start is called before the first frame update
    public MapBehaviour map;
    public Canvas pauseCanvas;
    [SerializeField] Animator m_fadeAnimation;
    protected PlayerInputAsset actions;
    // Update is called once per frame
    private void Start()
    {
        actions = new PlayerInputAsset();
        actions.PlayerInputActions.Enable();
    }
    void Update()
    {
        if (actions.PlayerInputActions.MapButton.triggered)
        {
            ShowMap();
  
        }
        if (actions.PlayerInputActions.PauseButton.triggered)
        {
            ShowPause();
        }   
    }
    public void ShowMap()
    {     
        map.GetComponent<Canvas>().enabled = !map.GetComponent<Canvas>().enabled;
        GameManager.Instance.PauseGame();
    }
    public void ShowPause()
    {
        pauseCanvas.enabled = !pauseCanvas.enabled;
        GameManager.Instance.PauseGame();
    }

    public void Fade()
    {
        m_fadeAnimation.SetTrigger("FadeTrigger");
    }
    public bool IsScreenOnBlack()
    {
        if (m_fadeAnimation.GetComponent<Image>().color.a >= 0.9f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
