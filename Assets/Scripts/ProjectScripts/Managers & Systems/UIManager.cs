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
    public static Animator minimapArrow;
    [SerializeField]
    private Animator m_minimapArrow;


    // Update is called once per frame
    private void Start()
    {
        minimapArrow = m_minimapArrow;
    }
    void Update()
    {
        if (InputManager.Instance.MapButtonTriggered())
        {
            ShowMap();
        }
        if (InputManager.Instance.PauseButtonTriggered())
        {
            ShowPause();
        }   
    }
    public void ShowMap()
    {     
        map.GetComponent<Canvas>().enabled = !map.GetComponent<Canvas>().enabled;
        GameManager.Instance.PauseGame();
        Cursor.visible = !Cursor.visible;

    }
    public void ShowPause()
    {
        pauseCanvas.enabled = !pauseCanvas.enabled;
        GameManager.Instance.PauseGame();
        Cursor.visible = !Cursor.visible;
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
