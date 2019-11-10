using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetArmAnimationValues : MonoBehaviour
{
    Animator myAnimator;
    [SerializeField] Transform m_transformRef;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        myAnimator.SetFloat("Angle", m_transformRef.localEulerAngles.z);
    }
}
