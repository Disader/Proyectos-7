using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public void DamagePlayer(int damage)
    {
        if ((!m_isInvulnerable))
        {          
            //El orden en importante, para que no mate al jugador se inicie después la coroutina, haciendo que respawnee con invulnerabilidad
            actualInvulnerabilityCoroutine = StartCoroutine(StartInvulnerability());
            HealthHeartsVisual.healthHeartsSystemStatic.Damage(damage);
            ///////////////////
        }
    }
    private void Update()
    {
        if (isOnFire)
        {
            timer += Time.deltaTime;
        }
        if (m_isInvulnerable)
        {
            m_invulnerableTimer += Time.deltaTime;
        }
    }

    //Fire State
    [Header("Variables de estado de fuego")]
    [HideInInspector] public Coroutine actualReciveDamageCoroutine;
    [SerializeField] ParticleSystem fireParticles;

    float timer = 0;
    bool isOnFire;

    public IEnumerator RecieveDamageOverTime(int damagePerSecond, float time)
    {
        isOnFire = true;
        timer = 0;
        fireParticles.Play();
        while (time > timer)
        {
            HealthHeartsVisual.healthHeartsSystemStatic.Damage(damagePerSecond);
            yield return new WaitForSeconds(1);
        }
        fireParticles.Stop();
        isOnFire = false;
        timer = 0;
        actualReciveDamageCoroutine = null;
    }
    public void RestartFireCoroutineTime()
    {
        timer = 0;
    }
    public void StopBurningPlayer()
    {
        if (actualReciveDamageCoroutine != null)
        {
            StopCoroutine(actualReciveDamageCoroutine);
        }
        timer = 0;
        fireParticles.Stop();
        isOnFire = false;
        actualReciveDamageCoroutine = null;
    }

    //Invulnerability
    [Header("Variables de invencibilidad")]
    [SerializeField] float invulnerabilityTime;
    [SerializeField] SpriteRenderer rendererToFlash;
    [SerializeField] float flashTime;

    bool m_isInvulnerable = false;
    float m_invulnerableTimer;

    Coroutine actualFlashCoroutine;
    Coroutine actualInvulnerabilityCoroutine;
    public IEnumerator StartInvulnerability()
    {
        m_isInvulnerable = true;
        actualFlashCoroutine=StartCoroutine(Flash());
        yield return new WaitForSeconds(invulnerabilityTime);
        rendererToFlash.enabled = true;
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
    }
    public void StopInvulnerabilityState()
    {
        if (actualFlashCoroutine != null)
        {
            StopCoroutine(actualFlashCoroutine);
        }
        if (actualInvulnerabilityCoroutine != null)
        {
            StopCoroutine(actualInvulnerabilityCoroutine);
        }
        rendererToFlash.enabled = true;
        m_isInvulnerable = false;
        m_invulnerableTimer = 0;
    }

    public void ResetPlayerStates()
    {
        //FireState
        StopBurningPlayer();
        //Invulnerability
        StopInvulnerabilityState();
    }
}
