using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChangerTrigger : MonoBehaviour
{
    [SerializeField] bool imRight;
    [SerializeField] RoomChangerObject roomChanger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerControl_MovementController>() == GameManager.Instance.ActualPlayerController && !roomChanger.m_isSaveRoomChanger)
        {
            if (imRight)
            {
                roomChanger.MovePlayerFromRight(collision.gameObject);
            }
            else
            {
                roomChanger.MovePlayerFromLeft(collision.gameObject);
            }
        }
        EnemySetControl enemy = collision.GetComponent<EnemySetControl>();
        if (roomChanger.m_isSaveRoomChanger)
        {
            if (enemy != null)
            {
                enemy.StartCoroutine(enemy.ConsumeEnemy());
            }
            
            if (imRight)
            {
                roomChanger.MovePlayerFromRight(collision.gameObject);
            }
            else
            {
                roomChanger.MovePlayerFromLeft(collision.gameObject);
            }
        }
    }
}
