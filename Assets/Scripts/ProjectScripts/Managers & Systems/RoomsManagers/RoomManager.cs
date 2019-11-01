using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera m_roomCamera;

    List<EnemyControl_MovementController> currentEnemiesInRoom = new List<EnemyControl_MovementController>();
    Dictionary<Transform,Vector3> originalEnemiesAtRoomPosition = new Dictionary<Transform, Vector3>();

    List<BulletBase> activeBulletsInRoom = new List<BulletBase>();

    void Awake()
    {
        currentEnemiesInRoom.AddRange(GetComponentsInChildren<EnemyControl_MovementController>());
        foreach(EnemyControl_MovementController enemy in currentEnemiesInRoom)
        {
            originalEnemiesAtRoomPosition.Add(enemy.transform, enemy.transform.position);
            enemy.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerControl_MovementController>() == GameManager.Instance.ActualPlayerController)
        {
            ChangeCamera();
            ZoneManager.Instance.SetNewActiveRoom(this);
            if (collision.GetComponent<EnemyControl_MovementController>() != null)
            {
                AddEnemyAtRoom(collision.GetComponent<EnemyControl_MovementController>());
            }
        }
        //Añado las balas al crearlas
        if (collision.GetComponent<BulletBase>() != null)
        {
            activeBulletsInRoom.Add(collision.GetComponent<BulletBase>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Elimino las balas al destruirse
        if (collision.GetComponent<BulletBase>() != null)
        {
            activeBulletsInRoom.Remove(collision.GetComponent<BulletBase>());
        }
    }
    public void AddEnemyAtRoom(EnemyControl_MovementController newEnemy)
    {
        if (!currentEnemiesInRoom.Contains(newEnemy))
        {
            currentEnemiesInRoom.Add(newEnemy);
        }   
    }
    public void RemoveEnemyAtRoom(EnemyControl_MovementController deleteEnemy)
    {
        if (currentEnemiesInRoom.Contains(deleteEnemy))
        {
            currentEnemiesInRoom.Remove(deleteEnemy);
        }
    }
    void ChangeCamera()
    {
        m_roomCamera.gameObject.SetActive(true);
        ZoneManager.Instance.SetActiveCamera(m_roomCamera);       
    }
    public void ActivateEnemies()
    {
        foreach (EnemyControl_MovementController enemy in currentEnemiesInRoom)
        {
            enemy.gameObject.SetActive(true);
        }
    }

    public void DeactivateEnemies()
    {
        foreach (EnemyControl_MovementController enemy in currentEnemiesInRoom)
        {
            if(enemy != GameManager.Instance.ActualPlayerController)
            {
                enemy.gameObject.SetActive(false);
            }
        }
    }
    public void DeleteControlledEnemyFromRoomList()
    {
        EnemyControl_MovementController currentControlledEnemy = null;

        foreach (EnemyControl_MovementController enemy in currentEnemiesInRoom)
        {
            if (enemy == GameManager.Instance.ActualPlayerController)
            {
                currentControlledEnemy = enemy;  //Es el jugador, así que se saca de la lista de enemigos en la sala
                break;
            }
        }
        if (currentControlledEnemy != null)
        {
            currentEnemiesInRoom.Remove(currentControlledEnemy);
        }
    }
    public void ResetRoom()
    {
        foreach(PlayerControl_MovementController enemy in currentEnemiesInRoom)
        {
            if (originalEnemiesAtRoomPosition.ContainsKey(enemy.transform))
            {
                enemy.transform.position = originalEnemiesAtRoomPosition[enemy.transform];
            }
        }

        //Reseteo de balas en la sala
        int bulletsInRoom = activeBulletsInRoom.Count;
        for( int i = 0; i < bulletsInRoom; i++)
        {
            Destroy(activeBulletsInRoom[i]);
        }
        activeBulletsInRoom.Clear();
    }
}
