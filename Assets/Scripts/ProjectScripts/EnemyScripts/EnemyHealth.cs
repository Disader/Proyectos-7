using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("La Vida del Enemigo")]
    public int enemyHealth;
    [HideInInspector]public int defaultEnemyHealth;

    [Header("El SetControl del Enemigo")]
    private EnemySetControl thisEnemySetControl;

    [Header("Textura para hacer el flash")]
    [SerializeField] Animator myAnimator;
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
    private void Update()
    {
        if (isOnFire)
        {
            recieveDamageTimer += Time.deltaTime;
        }
        if (m_isInvulnerable == true)
        {
            m_invulnerableTimer += Time.deltaTime;
        }
    }

    public void ReceiveDamage(int damageReceived) ////Recibir daño
    {
        if ((!m_isInvulnerable||isOnFire) && this.gameObject.GetComponent<EnemySetControl>().isBeingPossessed)
        {
            enemyHealth -= damageReceived;
            myAnimator.SetTrigger("Damaged"); //Animación de daño //PLACEHOLDER
            if (enemyHealth <= 0)
            {
                thisEnemySetControl.CheckEnemyDeath(); ////Al tener vida 0 se manda al SetControl chequear la muerte
            }
            else
            {
                StartCoroutine(StartInvulnerability());
            }
        }

        else //Recibir daño cuando no está poseído
        {
            enemyHealth -= damageReceived;
            myAnimator.SetTrigger("Damaged"); //Animación de daño //PLACEHOLDER

            if (enemyHealth <= 0)
            {
                thisEnemySetControl.CheckEnemyDeath(); ////Al tener vida 0 se manda al SetControl chequear la muerte
            }
        }
    }

    [HideInInspector] public Coroutine actualReciveDamageCoroutine;
    float recieveDamageTimer = 0;
    bool isOnFire;
    [SerializeField] ParticleSystem fireParticle;

    public IEnumerator recieveDamageOverTime(int damagePerSecond, float time)
    {
        isOnFire = true;
        recieveDamageTimer = 0;
        fireParticle.Play();
        while (time > recieveDamageTimer)
        {
            ReceiveDamage(damagePerSecond);
            yield return new WaitForSeconds(1);
        }
        fireParticle.Stop();
        isOnFire = false;
        recieveDamageTimer = 0;
    }

    [Header("Variables de invencibilidad")]
    bool m_isInvulnerable = false;
    float m_invulnerableTimer;

    [SerializeField] float invulnerabilityTime;
    [SerializeField] SpriteRenderer rendererToFlash;
    [SerializeField] float flashTime;
    public IEnumerator StartInvulnerability()
    {
        m_isInvulnerable = true;
        StartCoroutine(Flash());
        yield return new WaitForSeconds(invulnerabilityTime);
        m_isInvulnerable = false;
    }
    IEnumerator Flash()
    {
        m_invulnerableTimer = 0;

        while (invulnerabilityTime > m_invulnerableTimer)
        {
            rendererToFlash.enabled = !rendererToFlash.enabled;
            yield return new WaitForSeconds(flashTime);
        }
        rendererToFlash.enabled = true;
    }
   
}
