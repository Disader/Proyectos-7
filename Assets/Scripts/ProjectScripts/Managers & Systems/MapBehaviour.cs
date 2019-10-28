using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MapBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> unchartedRooms;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DiscoverRoom(GameObject roomToDiscover) 
    {
        for (int i = 0; i < unchartedRooms.Count; i++)
        {
            if(unchartedRooms[i] = roomToDiscover)
            {
                unchartedRooms[i].GetComponent<Image>().enabled = true;
                break;
            }
        }
    }
}
