using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MapBehaviour : MonoBehaviour
{
    public List<Image> mapRooms;
    public void DiscoverRoom(int currentRoom) 
    {
        mapRooms[currentRoom].enabled = true;
    }
}
