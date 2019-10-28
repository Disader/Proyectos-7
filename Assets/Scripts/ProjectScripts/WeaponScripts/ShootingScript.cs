﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    [SerializeField] float m_firingRate;
    float m_firingTimer;
    [SerializeField] GameObject m_bullet;
    [SerializeField] Transform m_shootingPos;

    public void FireInShootingPos()
    {
        m_firingTimer += Time.deltaTime;

        if (m_firingTimer > m_firingRate)
        {
            m_firingTimer = 0;
            GameObject obj = Instantiate(m_bullet, m_shootingPos.position,m_shootingPos.rotation);
        }
    }
}
