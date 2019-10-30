using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAbility : MonoBehaviour
{

    public PasiveAbility_SO currentPasiveAbility;
    
    public void SaveAbility()  ////AUN SIN RELACION CON SHOOTING SCRIPT
    {
        AbilitiesSlotsManager.Instance.currentSavedAbility = currentPasiveAbility;
    }
}
