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
        if (collision.GetComponent<EnemyControl_MovementController>() ==null && roomChanger.m_isSaveRoomChanger)
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
    }
}
