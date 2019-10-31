using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAbility : MonoBehaviour ////AUN SIN RELACION CON SHOOTING SCRIPT
{
    [Header("La Pasiva que va a tener el enemigo")]
    public PasiveAbility_SO enemyPasiveAbility;  ////La pasiva de este enemigo
    [Header("No tocar en Editor")]
    public PasiveAbility_SO currentActiveAbility;  ////La pasiva activa en el enemigo, que estaba guardada en el Manager
    
    public void SaveAbility()  
    {
        AbilitiesSlotsManager.Instance.StoreAbility(enemyPasiveAbility);
    }
}
