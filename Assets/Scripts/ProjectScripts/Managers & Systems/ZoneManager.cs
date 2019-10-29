using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : TemporalSingleton<ZoneManager>
{
    public MapBehaviour mapManager;
    public List<GameObject> rooms;
    void DiscoverRoom(GameObject currentRoom)
    {
        mapManager.DiscoverRoom(currentRoom);
    }
}
