using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetArmAnimationValues : MonoBehaviour
{
    Animator myAnimator;
    SpriteRenderer mySpriteRenderer;
    [SerializeField] Transform m_transformRef;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_transformRef.localEulerAngles.z < 337.5f && m_transformRef.localEulerAngles.z > 202.5f)
        {
            mySpriteRenderer.sortingOrder = 1000000;
        }
        else
        {
            mySpriteRenderer.sortingOrder = -1000000;
        }
        myAnimator.SetFloat("Angle", m_transformRef.localEulerAngles.z);

    }
}
