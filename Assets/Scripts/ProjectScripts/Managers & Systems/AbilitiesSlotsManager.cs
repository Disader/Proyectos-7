using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesSlotsManager : TemporalSingleton<AbilitiesSlotsManager>
{
    [Header("La Habilidad Pasiva que está Guardada")]
    public PasiveAbility_SO currentSavedAbility;
    [Header("El Número de Slots Disponibles")]
    public int slotCount = 1;

    public override void Awake()
    {
        base.Awake();
    }

    public void StoreAbility(PasiveAbility_SO abilityToStore)
    {
        if(abilityToStore.slotCost <= slotCount)
        {
            currentSavedAbility = abilityToStore;
        }

        else
        {
            Debug.Log("No hay suficientes slots para guardar Pasiva!");
        }
    }
}
