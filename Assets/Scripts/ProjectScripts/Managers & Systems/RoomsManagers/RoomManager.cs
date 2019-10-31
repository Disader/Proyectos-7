using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class OriginalEnemiesAtRoom
{
    public Transform position;
    public GameObject sceneObject;
}

public class RoomManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera m_roomCamera;
    List<EnemyControl_MovementController> enemiesInRoom = new List<EnemyControl_MovementController>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerControl_MovementController>() == GameManager.Instance.ActualPlayerController)
        {
            ChangeCamera();
            ZoneManager.Instance.SetNewActiveRoom(this);
           
        }
        //Set de los enemigos en la habitación al principio de la partida en el trigger, quizás un poco inestable pero fácil implementación de diseño
        if (collision.GetComponent<EnemyControl_MovementController>()!=null)
        {
            AddEnemyAtRoom(collision.GetComponent<EnemyControl_MovementController>());     
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
    void ResetRoom()
    {
        
    }


}
