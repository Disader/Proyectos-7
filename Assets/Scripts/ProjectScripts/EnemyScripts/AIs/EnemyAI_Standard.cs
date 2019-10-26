using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Standard : MonoBehaviour
{
    [SerializeField] float m_distanceToPlayer;
    [SerializeField] float m_playerDetectionDistance;
    [SerializeField] LayerMask m_sightCollisionMask;

    [SerializeField] Transform m_childSprite;


    enum AIState
    {
        idle,
        attackingPlayer
    }

    AIState currentAIState = AIState.idle;

    // Update is called once per frame
    void Update()
    {
        if (currentAIState == AIState.idle)
        {
            DetectPlayer();
        }

        RandomPointOnCircleEdge(m_distanceToPlayer);

        //Asegurarse de girar al final, tras haber hecho los cálculos del pathfinding
        m_childSprite.localRotation=Quaternion.Euler(new Vector3(90, 0, 0));
    }
    void DetectPlayer()
    {
        Vector2 vectorToPlayer = GameManager.Instance.ActualPlayerController.transform.position - this.transform.position;
        float distanceToPlayer= vectorToPlayer.magnitude;
        bool hasPlayerOnSight;
        if (Physics2D.Raycast(transform.position, vectorToPlayer, distanceToPlayer, m_sightCollisionMask))
        {
            hasPlayerOnSight = true;
        }
        else
        {
            hasPlayerOnSight = false;
        }
        Debug.DrawRay(transform.position, vectorToPlayer * distanceToPlayer, Color.red);

        if (distanceToPlayer>m_playerDetectionDistance||hasPlayerOnSight)
        {
            currentAIState = AIState.attackingPlayer;
        }
    }
    private Vector3 RandomPointOnCircleEdge(float radius)
    {
        var vector2 = Random.insideUnitCircle.normalized * radius;
        return new Vector3(vector2.x, vector2.y, 0);
    }
}
