using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChangerObject : MonoBehaviour
{
    [SerializeField] Transform m_rightTransform;
    [SerializeField] Transform m_leftTransform;


    public bool m_isSaveRoomChanger;

    public void MovePlayerFromRight(GameObject player)
    {
        player.transform.position = m_leftTransform.position;
    }
    public void MovePlayerFromLeft(GameObject player)
    {
        player.transform.position = m_rightTransform.position;
    }
}
