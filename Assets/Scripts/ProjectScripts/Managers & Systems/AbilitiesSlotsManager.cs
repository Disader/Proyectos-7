using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesSlotsManager : TemporalSingleton<AbilitiesSlotsManager>
{
    [Header("La Habilidad Pasiva que está Guardada")]
    public PasiveAbility_SO currentStoredAbility;
    [Header("El Número de Slots Disponibles")]
    public int slotCount = 1;

    public void StoreAbility(PasiveAbility_SO abilityToStore)
    {
        if (abilityToStore != null)
        {
            if (abilityToStore.slotCost <= slotCount)
            {
                currentStoredAbility = abilityToStore;
            }

            else
            {
                Debug.Log("No hay suficientes slots para guardar Pasiva!");
            }
        }
        else
        {
            currentStoredAbility = null;
        }
    }
   
}
