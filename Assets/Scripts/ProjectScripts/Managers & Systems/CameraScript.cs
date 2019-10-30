using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraScript : MonoBehaviour
{
    CinemachineVirtualCamera m_camera;
    private void Awake()
    {
        m_camera = GetComponent<CinemachineVirtualCamera>();
    }
    private void Update()
    {
        m_camera.Follow=GameManager.Instance.ActualPlayerController.transform;
    }
}
