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
    [SerializeField] float m_randomFiringRateDeviation;
    float m_firingRateTimer=1000f; //Para que comience disparando
    float m_onStartFiringRate;
    [Header("Variables Generales de Disparo")]
    [SerializeField] GameObject m_bullet;
    [SerializeField] Transform m_shootingPos;

    [Header("Variables del Disparo del Jugador")]//Para el control del jugador, afectado por más parámetros y/o mejoras.
    public float player_firingRate;
    float player_firingRateTimer;
    bool playerCanShoot;

    [Header("Partículas (PlaceHolder)")]
    public GameObject shootPart;

    private void Start()
    {
        m_onStartFiringRate = m_firingRate;
    }

    private void OnEnable()
    {
        player_firingRateTimer = 0;
    }

    public void FireInShootingPos(whoIsShooting shooter)
    {
        if (shooter == whoIsShooting.enemy) ////Como dispara el enemigo
        {
            m_firingRateTimer += Time.deltaTime;

            if (m_firingRateTimer > m_firingRate)
            {
                m_firingRate = Random.Range(m_onStartFiringRate - m_randomFiringRateDeviation, m_onStartFiringRate + m_randomFiringRateDeviation);
                m_firingRateTimer = 0;
                GameObject obj = Instantiate(m_bullet, m_shootingPos.position, m_shootingPos.rotation);         
                obj.layer = 10; ////Se le pone a la bala la Layer de BulletEnemy

                ////////EFECTOS///////
                Instantiate(shootPart, m_shootingPos.position, m_shootingPos.rotation);     //PLACEHOLDER
                 ///Sonido
                PlayRandomFiringSound();
            }
        }

        else if(shooter == whoIsShooting.player) //Como dispara el player al controlar este enemigo. Si hay pasiva, se afecta el tipo de bala.
        {

            if (player_firingRateTimer <= 0)
            {
                if (shSCR_PasiveAbility == null) ///Dispara bala normal si NO hay PASIVA ACTIVA
                {
                    GameObject obj = Instantiate(m_bullet, m_shootingPos.position, m_shootingPos.rotation);       
                    obj.layer = 12; ////Se le pone a la bala la Layer de BulletPlayer

                    ////////EFECTOS////////////
                    Instantiate(shootPart, m_shootingPos.position, m_shootingPos.rotation);     //PLACEHOLDER
                     ///Sonido
                    PlayRandomFiringSound();
                }

                else ////Si SÍ hay PASIVA ACTIVA, se instancia la bala que está guardada en la pasiva
                {
                    GameObject obj = Instantiate(shSCR_PasiveAbility.bulletType, m_shootingPos.position, m_shootingPos.rotation);
                    obj.layer = 12; ////Se le pone a la bala la Layer de BulletPlayer

                    ////////EFECTOS////////////
                    Instantiate(shootPart, m_shootingPos.position, m_shootingPos.rotation);     //PLACEHOLDER
                     ///Sonido
                    PlayRandomFiringSound();
                }
                player_firingRateTimer = player_firingRate;
            }

            else
            {
                player_firingRateTimer -= 1 * Time.deltaTime;
            }
        }
    }
    void PlayRandomFiringSound()
    {
        ///Sonido
        int random = Random.Range(0, 2);
        switch (random)
        {
            case 0:
                MusicManager.Instance.PlaySound(AppSounds.ENEMY_FIRE1); ///// PLACEHOLDER
                break;
            case 1:
                MusicManager.Instance.PlaySound(AppSounds.ENEMY_FIRE2); ///// PLACEHOLDER
                break;
            default:
                break;
        }
    }
    public void ResetPlayerFiringRateTimer() //Método que se llama desde EnemyController si no hay input de RightTrigger
    {
        if (player_firingRateTimer <= 0)
        {
            player_firingRateTimer = 0;
        }

        else
        {
            player_firingRateTimer -= 1 * Time.deltaTime;
        }
    }
}
