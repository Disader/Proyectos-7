using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : PersistentSingleton<RoomManager>
{
    public MapBehaviour mapManager;
    public List<GameObject> rooms;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DiscoverRoom(GameObject currentRoom)
    {
        mapManager.DiscoverRoom(currentRoom);
    }
}
