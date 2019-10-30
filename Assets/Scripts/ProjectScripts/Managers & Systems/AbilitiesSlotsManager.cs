using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesSlotsManager : TemporalSingleton<AbilitiesSlotsManager>
{
    public PasiveAbility_SO currentSavedAbility; 

    public override void Awake()
    {
        base.Awake();
    }
}
