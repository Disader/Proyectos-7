using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Cinemachine;

public class ZoneManager : TemporalSingleton<ZoneManager>
{
    public MapBehaviour mapManager;
    [HideInInspector] public CinemachineVirtualCamera m_activeCamera;
    [HideInInspector] public RoomManager m_activeRoom;

    [Header("Posición inicial del jugador al comenzar el nivel")]
    [SerializeField] Transform initialPlayerPosition;

    [Header("La Escena Contiene la Zona actual para cargar en Checkpoints")]
    public SceneReference zoneScene;

    public IEnumerator ChangeRoom(CinemachineVirtualCamera newCamera, RoomManager newRoom)
    {
              
        if (m_activeRoom != null && m_activeRoom!=newRoom)
        {
            UIManager.Instance.Fade();
            GameManager.Instance.PauseGame();
            yield return new WaitUntil(() => UIManager.Instance.IsScreenOnBlack());
            GameManager.Instance.PauseGame();

        }
        if (m_activeCamera != null && m_activeCamera != newCamera)
        {
            m_activeCamera.gameObject.SetActive(false);
        }
        m_activeCamera = newCamera;
        newCamera.gameObject.SetActive(true);

        SetNewActiveRoom(newRoom); //Cambio de habitación
    }

    public void SetNewActiveRoom(RoomManager newRoom)
    {
        if (m_activeRoom != newRoom)
        {
            if (m_activeRoom != null)
            {
                m_activeRoom.DeactivateEnemies();
                m_activeRoom.DeactivateSpawnersInRoom();
                m_activeRoom.DeleteControlledEnemyFromRoomList();
                m_activeRoom.ResetRoom();
            }
            m_activeRoom = newRoom;
            m_activeRoom.ActivateEnemies();
            m_activeRoom.ActivateSpawnersInRoom();
        }

    }
    public void DeleteEnemyFromCurrentRoom(EnemyControl_MovementController enemy)
    {
        m_activeRoom.RemoveEnemyAtRoom(enemy);
    }
    public void AddBulletInActiveRoom(BulletBase bullet)
    {
        m_activeRoom.AddBulletInRoom(bullet);
    }
    public void RemoveBulletInActiveRoom(BulletBase bullet)
    {
        m_activeRoom.RemoveBulletInRoom(bullet);
    }
    
    // Métodos creados para permitir setear la posición del jugador cuando se cambia de zona
    public void SetPlayerInPositionOnLevelStart(Transform newTransform)
    {
        GameManager.Instance.realPlayerGO.transform.position = newTransform.position;
    }
    public void SetPlayerInPositionOnLevelStart() 
    {
        GameManager.Instance.realPlayerGO.transform.position = initialPlayerPosition.position;
    }
}
