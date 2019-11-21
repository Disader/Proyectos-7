using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] float m_velocity;
    protected PlayerInputAsset actions;
    [SerializeField] float m_aimDistance;
    protected virtual void Start()
    {

        actions = new PlayerInputAsset();
        actions.PlayerInputActions.Enable();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 desiredPosition= (Vector2)GameManager.Instance.ActualPlayerController.transform.position + actions.PlayerInputActions.Rotating.ReadValue<Vector2>().normalized * m_aimDistance;
        transform.position = Vector2.MoveTowards(transform.position, desiredPosition, m_velocity * Time.deltaTime);
    }
}
