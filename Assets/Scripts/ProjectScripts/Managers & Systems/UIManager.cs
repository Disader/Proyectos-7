using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : TemporalSingleton<UIManager>
{
    // Start is called before the first frame update
    public MapBehaviour map;
    public Canvas pauseCanvas;
    [SerializeField] Animation m_fadeAnimation;
    protected PlayerInputAsset actions;
    // Update is called once per frame
    private void Start()
    {
        actions = new PlayerInputAsset();
        actions.PlayerInputActions.Enable();
    }
    void FixedUpdate()
    {
        if (actions.PlayerInputActions.MapButton.ReadValue<float>() != 0)
        {
            ShowMap();
        }
        if (actions.PlayerInputActions.PauseButton.ReadValue<float>() != 0)
        {
            ShowPause();
            GameManager.Instance.PauseGame(0);
        }
    }
    public void ShowMap()
    {     
        map.GetComponent<Canvas>().enabled = !map.GetComponent<Canvas>().enabled;
    }
    public void ShowPause()
    {
        pauseCanvas.enabled = !pauseCanvas.enabled;

    }
    public void Fade()
    {
        m_fadeAnimation.Play();
    }
    public bool IsScreenOnBlack()
    {
        if(m_fadeAnimation.GetComponent<Image>().color.a == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
      
    }
}
