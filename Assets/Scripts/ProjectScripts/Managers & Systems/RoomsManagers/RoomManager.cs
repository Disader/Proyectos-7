using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera m_roomCamera;

    [SerializeField] List<EnemyControl_MovementController> currentEnemiesInRoom;
    Dictionary<Transform,Vector3> originalEnemiesAtRoomPosition = new Dictionary<Transform, Vector3>();
    List<BulletBase> activeBulletsInRoom = new List<BulletBase>();

    BoxCollider2D myCollider;
    [SerializeField] LayerMask m_enemyLayer;
    void Awake()
    {     
        myCollider = GetComponent<BoxCollider2D>();
        Vector2 hitColliderPosition = new Vector2(gameObject.transform.position.x + myCollider.offset.x, gameObject.transform.position.y + myCollider.offset.y);
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(hitColliderPosition, myCollider.size, 0f, m_enemyLayer);

        int i = 0;
        while (i < hitColliders.Length)
        {
            EnemyControl_MovementController enemyController = hitColliders[i].GetComponent<EnemyControl_MovementController>();
            if (enemyController != null)
            {
                AddEnemyAtRoom(enemyController);
            }
            i++;
        }

        foreach (EnemyControl_MovementController enemy in currentEnemiesInRoom)
        {
            originalEnemiesAtRoomPosition.Add(enemy.transform, enemy.transform.position);
            enemy.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerControl_MovementController>() == GameManager.Instance.ActualPlayerController)
        {
            StartCoroutine(ZoneManager.Instance.SetActiveCamera(m_roomCamera,this)); //Cambio de la cámara
            ZoneManager.Instance.SetNewActiveRoom(this); //Cambio de habitación
            if (collision.GetComponent<EnemyControl_MovementController>() != null) 
            {
                AddEnemyAtRoom(collision.GetComponent<EnemyControl_MovementController>()); //Añadir enemigo si está siendo controlado por el jugador y entra a una nueva sala
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
            RemoveEnemyAtRoom(currentControlledEnemy);
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
