using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public enum whoIsShooting { enemy, player }; //Quien dispara

    [Header("La Pasiva que le pasa ActiveAbility")]
    [HideInInspector] public PasiveAbility_SO shSCR_PasiveAbility; //El contenido lo maneja ActiveAbility, al cual le llama EnemySetControl. No tocar

    [Header("Variables del Disparo Enemigo")] //Las que tu creaste para el enemigo Sergio
    [SerializeField] float m_firingRate;
    float m_firingRateTimer;
    [Header("Variables Generales de Disparo")]
    [SerializeField] GameObject m_bullet;
    [SerializeField] Transform m_shootingPos;

    [Header("Variables del Disparo del Jugador")]//Para el control del jugador, afectado por más parámetros y/o mejoras.
    public float player_firingRate;
    float player_firingRateTimer = 10000;
    bool playerCanShoot;

    public void FireInShootingPos(whoIsShooting shooter)
    {
        if (shooter == whoIsShooting.enemy) ////Como dispara el enemigo
        {
            m_firingRateTimer += Time.deltaTime;

            if (m_firingRateTimer > m_firingRate)
            {
                m_firingRateTimer = 0;
                GameObject obj = Instantiate(m_bullet, m_shootingPos.position, m_shootingPos.rotation);
            }
        }

        else if(shooter == whoIsShooting.player) //Como dispara el player al controlar este enemigo. Si hay pasiva, se afecta el tipo de bala.
        {

            if (player_firingRateTimer >= player_firingRate)
            {
                if (shSCR_PasiveAbility == null)
                {
                    GameObject obj = Instantiate(m_bullet, m_shootingPos.position, m_shootingPos.rotation);
                }

                else
                {
                    GameObject obj = Instantiate(shSCR_PasiveAbility.bulletType, m_shootingPos.position, m_shootingPos.rotation);
                }
                player_firingRateTimer = 0;
            }
            player_firingRateTimer += 1 * Time.deltaTime;
        }
    }

    public void ResetOnStopAttack() //Colocar aquí cosas que se quieran restear al dejar de pulsar el RigthTrigger en el control de enemigo
    {
        player_firingRateTimer = player_firingRate;
    }
}
