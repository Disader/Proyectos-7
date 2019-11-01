using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAbility : MonoBehaviour
{
    [Header("La Pasiva que va a tener el enemigo")]
    public PasiveAbility_SO enemyPasiveAbility;  ////La pasiva de este enemigo
    [Header("No tocar en Editor")]
    public PasiveAbility_SO currentActiveAbility;  ////La pasiva activa en el enemigo, que estaba guardada en el Manager
    
    public void SaveAbility()  //Guardar pasiva en Manager
    {
        AbilitiesSlotsManager.Instance.StoreAbility(enemyPasiveAbility);
    }

    public void SetCurrentAbility(ShootingScript enemyShootingScript) //Aplicar pasiva a la variable de currentActiveAbility, al script de disparo/ataque y eliminar la guardada en el manager
    {
        currentActiveAbility = AbilitiesSlotsManager.Instance.currentStoredAbility;
        enemyShootingScript.shSCR_PasiveAbility = currentActiveAbility;
        AbilitiesSlotsManager.Instance.currentStoredAbility = null;
    }

    public void EraseCurrentAbility(ShootingScript enemyShootingScript) //Al desposeer se elimina la pasiva de la variable currentActiveAbility y del script de disparo/ataque
    {
        currentActiveAbility = null;
        enemyShootingScript.shSCR_PasiveAbility = null;
    }
}
