using System.Collections;
using System.Collections.Generic;
using UnityEngine.Timeline;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    [Header("Playable a reproducir")]
    public TimelineAsset playable;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8 && playable != null)
        {
            CutsceneManager.Instance.PlayCutscene(0, playable);
            this.gameObject.SetActive(false);
        }
    }
}
