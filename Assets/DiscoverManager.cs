using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoverManager : TemporalSingleton<DiscoverManager>
{

    [Header("Lista de habitaciones")]
    public List<RoomManager> roomList = new List<RoomManager>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
