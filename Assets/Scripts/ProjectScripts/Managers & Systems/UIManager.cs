using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public MapBehaviour map;
    void Start()
    {
        
    }

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
    
}
