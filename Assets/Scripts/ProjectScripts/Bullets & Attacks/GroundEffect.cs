using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEffect : DamageObject
{
    [SerializeField] GroundEffect_SO groundEffectSO;
    Sprite m_sprite;
    Animator m_anime;
    AnimationClip m_animation;
    bool m_isFire;
    int m_damageByTime;
    float m_timeInFire;
    new int bulletDamageToPlayer;
    new int bulletDamageToEnemy;


    // Start is called before the first frame update
    void Start()
    {
        GetScriptable();
        m_anime = GetComponent<Animator>();      
        m_anime.Play(m_animation.name);
    }

    // Update is called once per frame

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(m_isFire)
        {
            CollisionWithPlayer(collision.gameObject.GetComponent<PlayerHealthController>());
        }
    }
    private void GetScriptable()
    {
        m_sprite = groundEffectSO.sprite;
        m_animation = groundEffectSO.animation;
        m_isFire = groundEffectSO.isFire;
        bulletDamageToPlayer = groundEffectSO.touchDamage;
        m_damageByTime = groundEffectSO.damageByTime;
        m_timeInFire = groundEffectSO.timeInFire;
        if(!m_isFire)
        {
            bulletDamageToEnemy = groundEffectSO.touchDamage;
        }
    }
    override protected void CollisionWithPlayer(PlayerHealthController playerHealth)
    {
        base.bulletDamageToPlayer = groundEffectSO.touchDamage;
        base.CollisionWithPlayer(playerHealth);
       
        if (playerHealth.actualReciveDamageCoroutine != null)
        {
            playerHealth.RestartFireCoroutineTime();
        }
        else if (m_isFire)
        {
            playerHealth.actualReciveDamageCoroutine = playerHealth.StartCoroutine(playerHealth.RecieveDamageOverTime(m_damageByTime, m_timeInFire));
        }
    }
}
