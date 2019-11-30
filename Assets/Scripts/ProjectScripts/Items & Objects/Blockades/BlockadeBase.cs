using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockadeBase : MonoBehaviour
{
    //Método base para romper bloqueo, heredado por resto de scripts.
    public virtual void BreakBlockade()
    {
        gameObject.SetActive(false);    //Faltan animaciones.
    }
}
