using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAbility : MonoBehaviour
{
    [Header("La Pasiva que va a tener el enemigo")]
    public PasiveAbility_SO enemyPasiveAbility;  ////La pasiva de este enemigo
    [HideInInspector]
    public PasiveAbility_SO currentActiveAbility;  ////La pasiva activa en el enemigo, que estaba guardada en el Manager
    
    public void SaveAbility()  //Guardar pasiva en Manager
    {
        AbilitiesSlotsManager.Instance.StoreAbility(enemyPasiveAbility);
    }

    public void SetCurrentAbility(ShootingScript enemyShootingScript) //Aplicar pasiva a la variable de currentActiveAbility y al script de disparo/ataque
    {
        if(enemyPasiveAbility != AbilitiesSlotsManager.Instance.currentStoredAbility) //Si la pasiva no es igual que la almacenada, para avitar aplicarla en el enemigo que la setea
        {
            currentActiveAbility = AbilitiesSlotsManager.Instance.currentStoredAbility;
            enemyShootingScript.shSCR_PasiveAbility = currentActiveAbility;
        }  
    }

    public void EraseCurrentAbility(ShootingScript enemyShootingScript) //Al desposeer se elimina la pasiva de la variable currentActiveAbility y del script de disparo/ataque
    {
        currentActiveAbility = null;
        enemyShootingScript.shSCR_PasiveAbility = null;
    }
}
