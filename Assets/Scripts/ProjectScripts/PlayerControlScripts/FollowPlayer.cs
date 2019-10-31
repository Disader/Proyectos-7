using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] float m_velocity;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,GameManager.Instance.ActualPlayerController.transform.position, m_velocity * Time.deltaTime);
    }
}
