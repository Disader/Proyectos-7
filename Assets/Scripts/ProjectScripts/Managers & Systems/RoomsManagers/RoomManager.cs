using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera m_roomCamera;
    List<EnemyControl_MovementController> enemiesInRoom = new List<EnemyControl_MovementController>();
    Dictionary<Transform,Vector3> originalEnemiesAtRoomPosition = new Dictionary<Transform, Vector3>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerControl_MovementController>() == GameManager.Instance.ActualPlayerController)
        {
            ChangeCamera();
            ZoneManager.Instance.SetNewActiveRoom(this);
           
        }
        EnemyControl_MovementController enemyController = collision.GetComponent<EnemyControl_MovementController>();
        //Set de los enemigos en la habitación al principio de la partida en el trigger, quizás un poco inestable pero fácil implementación de diseño
        if (enemyController != null)
        {
            AddEnemyAtRoom(enemyController);
            if (!originalEnemiesAtRoomPosition.ContainsKey(enemyController.transform))
            {
                originalEnemiesAtRoomPosition.Add(enemyController.transform, enemyController.transform.position);
            }
        }
    }
    public void AddEnemyAtRoom(EnemyControl_MovementController newEnemy)
    {
        if (!enemiesInRoom.Contains(newEnemy))
        {
            enemiesInRoom.Add(newEnemy);
        }   
    }
    public void RemoveEnemyAtRoom(EnemyControl_MovementController deleteEnemy)
    {
        if (enemiesInRoom.Contains(deleteEnemy))
        {
            enemiesInRoom.Remove(deleteEnemy);
        }
    }
    void ChangeCamera()
    {
        m_roomCamera.gameObject.SetActive(true);
        ZoneManager.Instance.SetActiveCamera(m_roomCamera);       
    }
    public void ActivateEnemies()
    {
        foreach (EnemyControl_MovementController enemy in enemiesInRoom)
        {
            enemy.gameObject.SetActive(true);
        }
    }

    List<EnemyControl_MovementController> enemiesToDelete = new List<EnemyControl_MovementController>();
    public void DeactivateEnemies()
    {
        foreach (EnemyControl_MovementController enemy in enemiesInRoom)
        {
            if(enemy != GameManager.Instance.ActualPlayerController)
            {
                enemy.gameObject.SetActive(false);
            }
            else
            {
                enemiesToDelete.Add(enemy);
            }
        }
        foreach (EnemyControl_MovementController enemy in enemiesToDelete)
        {
            RemoveEnemyAtRoom(enemy);
        }
        enemiesToDelete.Clear();
    }
    public void ResetRoom()
    {
        foreach(PlayerControl_MovementController enemy in enemiesInRoom)
        {
            if (originalEnemiesAtRoomPosition.ContainsKey(enemy.transform))
            {
                enemy.transform.position = originalEnemiesAtRoomPosition[enemy.transform];
            }
        }
    }
}
