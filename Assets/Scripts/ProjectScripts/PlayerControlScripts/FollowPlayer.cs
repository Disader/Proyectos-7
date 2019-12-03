using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] float m_velocity;
    [SerializeField] float m_aimDistance;

    // Update is called once per frame
    void Update()
    {
        Vector2 desiredPosition= (Vector2)GameManager.Instance.ActualPlayerController.transform.position + InputManager.Instance.RotationInput().normalized * m_aimDistance;
        transform.position = Vector2.MoveTowards(transform.position, desiredPosition, m_velocity * Time.deltaTime); ;
          
    }
    public void ResetPosition()
    {
        transform.position = GameManager.Instance.realPlayerGO.transform.position;
    }
}
