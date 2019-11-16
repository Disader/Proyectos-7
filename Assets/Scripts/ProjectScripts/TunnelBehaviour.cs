﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelBehaviour : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PossessAbility>() != null) ///Check de player mediante si tiene PossessAbility, que solo pertenece a Player.
        {
            GameManager.Instance.playerIsHidden = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PossessAbility>() != null)
        {
            GameManager.Instance.playerIsHidden = false;
        }
    }
}
