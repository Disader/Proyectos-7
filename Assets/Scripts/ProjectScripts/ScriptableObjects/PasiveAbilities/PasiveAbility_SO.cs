using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PasiveAbility", menuName = "PasiveAbility")]
public class PasiveAbility_SO : ScriptableObject
{
    ////Actualmente solo tiene información de Balas.
    ///
    [SerializeField]
    private GameObject m_bulletType;

    public GameObject bulletType
    {
        get
        {
            return m_bulletType;
        }
    }

    [SerializeField]
    private int m_slotCost;

    public int slotCost
    {
        get
        {
            return m_slotCost;
        }
    }
}
