using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : TemporalSingleton<UIManager>
{
    // Start is called before the first frame update
    public MapBehaviour map;
    [SerializeField] Animation m_fadeAnimation;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ShowMap();
        }
    }
    public void ShowMap()
    {     
        map.GetComponent<Canvas>().enabled = !map.GetComponent<Canvas>().enabled;
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
