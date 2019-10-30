using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera m_roomCamera;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerControl_MovementController>() == GameManager.Instance.ActualPlayerController)
        {
            ZoneManager.Instance.SetActiveCamera(m_roomCamera);
            m_roomCamera.gameObject.SetActive(true);
        }
       
    }


}
