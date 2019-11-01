using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RoomManagerEditor : MonoBehaviour
{
    RoomManager roomManager;
    BoxCollider2D myCollider;
    [SerializeField] LayerMask m_LayerMask;
    Vector3 m_center;
    Vector3 m_dimensions;

    private void Update()
    {
        roomManager = GetComponent<RoomManager>();
        myCollider = GetComponent<BoxCollider2D>();
        m_dimensions = myCollider.size;
        m_center = myCollider.offset;
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(gameObject.transform.position + m_center, m_dimensions, 0f, m_LayerMask);
        int i = 0;

        //Check when there is a new collider coming into contact with the box
        while (i < hitColliders.Length)
        {
            EnemyControl_MovementController enemyController = hitColliders[i].GetComponent<EnemyControl_MovementController>();
            if (enemyController != null)
            {
                roomManager.AddEnemyAtRoom(enemyController);
               
            }

            i++;
        }
    }

    //Draw the Box Overlap as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode

        Gizmos.DrawWireCube(gameObject.transform.position + m_center, m_dimensions);
    }
}
