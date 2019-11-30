using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public void DamagePlayer(int damage)
    {
        if ((!m_isInvulnerable))
        {
            HealthHeartsVisual.healthHeartsSystemStatic.Damage(damage);
            StartCoroutine(StartInvulnerability());
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
    [Header("Variables de estado de fuego")]
    [HideInInspector] public Coroutine actualReciveDamageCoroutine;
    float timer = 0;
    bool isOnFire;
    [SerializeField] ParticleSystem fireParticles;
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
    }
    public void StopBurningPlayer()
    {
        if (actualReciveDamageCoroutine != null)
        {
            StopAllCoroutines();
            fireParticles.Stop();
            isOnFire = false;
            timer = 0;
        }
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
