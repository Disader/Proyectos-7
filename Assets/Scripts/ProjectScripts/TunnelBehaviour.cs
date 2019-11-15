using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelBehaviour : MonoBehaviour
{
    [SerializeField]
    BoxCollider2D m_TunnelBlock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemySetControl>() != null)
        {
            m_TunnelBlock.enabled = true;
        }
        else 
        {
            m_TunnelBlock.enabled = false;
        }
            
    }
    private void OnTriggerExit(Collider other)
    {
        m_TunnelBlock.enabled = false;   
    }
}
