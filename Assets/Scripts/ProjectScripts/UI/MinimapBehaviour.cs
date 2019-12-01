using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MinimapBehaviour : MonoBehaviour
{
    [SerializeField]
    Image m_minimap;
    [SerializeField]
    GameObject m_playerArrow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_playerArrow.transform.position = GameManager.Instance.ActualPlayerController.gameObject.transform.position;
    }
}
