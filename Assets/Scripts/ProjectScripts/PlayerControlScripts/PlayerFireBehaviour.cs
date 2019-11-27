using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireBehaviour : MonoBehaviour
{
    [HideInInspector] public Coroutine actualReciveDamageCoroutine;
    float timer = 0;
    bool isOnFire;
    [SerializeField] ParticleSystem fireParticles;
    public IEnumerator recieveDamageOverTime(int damagePerSecond, float time)
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
    private void Update()
    {
        if (isOnFire)
        {
            timer += Time.deltaTime;
        }
    }
}
