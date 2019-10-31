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
        FollowPlayer followPlayer = FindObjectOfType<FollowPlayer>();
        if (followPlayer != null)
        {
            m_camera.Follow = followPlayer.transform;
        }
    }
}
