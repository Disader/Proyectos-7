using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("La Vida del Enemigo")]
    public int enemyHealth;
    private int defaultEnemyHealth;

    [Header("El SetControl del Enemigo")]
    private EnemySetControl thisEnemySetControl;

    void Awake()
    {
        defaultEnemyHealth = enemyHealth;   ////Se guarda la vida inicial del enemigo.
    }

    // Start is called before the first frame update
    void Start()
    {
        thisEnemySetControl = GetComponent<EnemySetControl>();
    }

    private void OnDisable()
    {
        enemyHealth = defaultEnemyHealth; ////Al activarse el enemigo, recupera la vida perdida
    }

    public void ReceiveDamage(int damageReceived) ////Recibir daño
    {
        enemyHealth -= damageReceived;

        if (enemyHealth <= 0)
        {
            thisEnemySetControl.CheckEnemyDeath(); ////Al tener vida 0 se manda al SetControl chequear la muerte
        }
    }
}
