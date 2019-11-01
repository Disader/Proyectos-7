using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ZoneManager : TemporalSingleton<ZoneManager>
{
    public MapBehaviour mapManager;
    public List<RoomManager> rooms = new List<RoomManager>();
    CinemachineVirtualCamera m_activeCamera;
    RoomManager m_activeRoom;
    private void Start()
    {
        rooms.AddRange(FindObjectsOfType<RoomManager>());
        foreach(RoomManager room in rooms)
        {
            room.DeactivateEnemies();
            Debug.Log("deactivate");
        }
    }
    void DiscoverRoom(GameObject currentRoom)
    {
        mapManager.DiscoverRoom(currentRoom);
    }
    public void SetActiveCamera(CinemachineVirtualCamera newCamera)
    {
        if (m_activeCamera != null && m_activeCamera!=newCamera)
        {
            m_activeCamera.gameObject.SetActive(false);
        }
        m_activeCamera = newCamera;
    }
    public void SetNewActiveRoom(RoomManager newRoom)
    {
        if (m_activeRoom != newRoom)
        {
            if (m_activeRoom != null)
            {
                m_activeRoom.DeactivateEnemies();
                m_activeRoom.ResetRoom();
            }
            m_activeRoom = newRoom;
            m_activeRoom.ActivateEnemies();
            Debug.Log("activate");
            
        }

    }
}
