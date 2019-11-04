using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ZoneManager : TemporalSingleton<ZoneManager>
{
    public MapBehaviour mapManager;
    CinemachineVirtualCamera m_activeCamera;
    RoomManager m_activeRoom;

    void DiscoverRoom(GameObject currentRoom)
    {
        mapManager.DiscoverRoom(currentRoom);
    }
    public IEnumerator SetActiveCamera(CinemachineVirtualCamera newCamera, RoomManager newRoom)
    {
        if (m_activeRoom != null && m_activeRoom!=newRoom)
        {
            Debug.Log("olakease");
            UIManager.Instance.Fade();
            yield return new WaitUntil(() => UIManager.Instance.IsScreenOnBlack());
            
        }
        if (m_activeCamera != null && m_activeCamera != newCamera)
        {
            m_activeCamera.gameObject.SetActive(false);
        }
        m_activeCamera = newCamera;
        newCamera.gameObject.SetActive(true);
    }

    public void SetNewActiveRoom(RoomManager newRoom)
    {
        if (m_activeRoom != newRoom)
        {
            if (m_activeRoom != null)
            {
                m_activeRoom.DeactivateEnemies();
                m_activeRoom.DeleteControlledEnemyFromRoomList();
                m_activeRoom.ResetRoom();
            }
            m_activeRoom = newRoom;
            m_activeRoom.ActivateEnemies();           
        }

    }
}
