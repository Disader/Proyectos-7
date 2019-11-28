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

    public void ReceiveDamage(int damageReceived) ////Recibir daño
    {
        enemyHealth -= damageReceived;
        myAnimator.SetTrigger("Damaged"); //Animación de daño //PLACEHOLDER
        if (enemyHealth <= 0)
        {
            thisEnemySetControl.CheckEnemyDeath(); ////Al tener vida 0 se manda al SetControl chequear la muerte
        }
    }
    [HideInInspector] public Coroutine actualReciveDamageCoroutine;
    float timer = 0;
    bool isOnFire;
    [SerializeField] ParticleSystem fireParticle;
    public IEnumerator recieveDamageOverTime(int damagePerSecond, float time)
    {
        isOnFire = true;
        timer = 0;
        fireParticle.Play();
        while (time > timer)
        {
            ReceiveDamage(damagePerSecond);
            yield return new WaitForSeconds(1);
        }
        fireParticle.Stop();
        isOnFire = false;
        timer = 0;
    }

    private void Update()
    {
        if (isOnFire)
        {
            timer += Time.deltaTime;
        }
    }
}
