using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public enum whoIsShooting { enemy, player }; //Quien dispara

    [HideInInspector] public PasiveAbility_SO shSCR_PasiveAbility; //El contenido lo maneja ActiveAbility, al cual le llama EnemySetControl. No tocar
    [Header("Variables de detección de la IA")]
    [SerializeField] float m_weaponHearingDistance;
    [SerializeField] LayerMask m_enemyLayer=9;

    [Header("Variables Generales de Disparo")]
    [SerializeField] GameObject m_bullet;
    [SerializeField] Transform m_shootingPos;
    [SerializeField] bool m_killAll; //Para saber si mata tanto al jugador como a los enemigos.
    [SerializeField] int m_bulletsToInstantiateAtOnce=1;
    [SerializeField] float m_dispersionAngle;
    [SerializeField] float m_shootingAngle;//Para cuando se disparan varias balas, el ángulo en el que se reparten equitativamente
    [SerializeField] int m_bulletsPerBurst=1;
    [SerializeField] float m_burtFiringRate;

    [Header("Variables del Disparo Enemigo")]
    [SerializeField] float m_firingRate;
    [SerializeField] float m_randomFiringRateDeviation;

    float m_firingRateTimer=0; //Para que comience disparando
    float m_onStartFiringRate;


    [Header("Variables del Disparo del Jugador")]//Para el control del jugador, afectado por más parámetros y/o mejoras.
    [SerializeField] float player_firingRate;

    float player_firingRateTimer;


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
            EnemyShoot();
        }

        else if(shooter == whoIsShooting.player) //Como dispara el player al controlar este enemigo. Si hay pasiva, se afecta el tipo de bala.
        {
            PlayerShoot();
        }
    }
    void EnemyShoot()
    {
        m_firingRateTimer -= Time.deltaTime;

        if (m_firingRateTimer <= 0)
        {
            m_firingRate = Random.Range(m_onStartFiringRate - m_randomFiringRateDeviation, m_onStartFiringRate + m_randomFiringRateDeviation);
            m_firingRateTimer = m_firingRate;

            StartCoroutine(BurstShot(m_bullet, 10));
        }
    }
   
    void PlayerShoot()
    {
        if (player_firingRateTimer <= 0)
        {
            if (shSCR_PasiveAbility == null) ///Dispara bala normal si NO hay PASIVA ACTIVA
            {
                StartCoroutine(BurstShot(m_bullet, 12));
            }
            else
            {
                StartCoroutine(BurstShot(shSCR_PasiveAbility.bulletType, 12));
            }
            
            player_firingRateTimer = player_firingRate;
        }
        else
        {
            player_firingRateTimer -= 1 * Time.deltaTime;
        }
    }
    IEnumerator BurstShot(GameObject bullet, int layer)
    {
        for(int i=0; i< m_bulletsPerBurst; i++)
        {
            InstantiateBullet(bullet,layer);
            yield return new WaitForSeconds(m_burtFiringRate);
        }
    }
    void InstantiateBullet(GameObject bullet,int layer)
    {
        //Angle calculations
        float angleDiference = m_shootingAngle / m_bulletsToInstantiateAtOnce;
        int subdivisions = Mathf.FloorToInt(m_bulletsToInstantiateAtOnce / 2);
        float initialShootingAngle = -angleDiference * subdivisions;

        for (int i = 0; i < m_bulletsToInstantiateAtOnce; i++)
        {
            float randomAngleDispersion = Random.Range(-m_dispersionAngle, m_dispersionAngle);
            GameObject obj = Instantiate(bullet, m_shootingPos.position, m_shootingPos.rotation * Quaternion.AngleAxis(randomAngleDispersion + initialShootingAngle + i * angleDiference, Vector3.forward));

            if (!m_killAll)
            {
                obj.layer = layer; ////Se le pone a la bala la layer correspondiente
            }
        }
        AlertEnemies();
        ShootingEffects();
    }
    void AlertEnemies()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.position, m_weaponHearingDistance, m_enemyLayer);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            EnemyAI_Standard enemyAI = hitColliders[i].GetComponent<EnemyAI_Standard>();
            if (enemyAI != null)
            {
                enemyAI.ExternalDetectPlayer();
            }
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
    void ShootingEffects()
    {
        Instantiate(shootPart, m_shootingPos.position, m_shootingPos.rotation);     //PLACEHOLDER
                                                                                    ///Sonido
        PlayRandomFiringSound();
    }
}
