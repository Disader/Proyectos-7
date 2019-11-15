using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSound : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    float counter;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (myRigidbody.velocity.magnitude > 0)
        {
            counter += Time.deltaTime;
            if (counter > 0.3f)
            {
                PlayRandomSound();
                counter = 0;
            }
        }
    }
    void PlayRandomSound()
    {
        int random = Random.Range(0, 9);
        switch (random)
        {
            case 0:
                MusicManager.Instance.PlaySound(AppSounds.PLAYER_MOV1); ///// PLACEHOLDER
                break;
            case 1:
                MusicManager.Instance.PlaySound(AppSounds.PLAYER_MOV2); ///// PLACEHOLDER
                break;
            case 2:
                MusicManager.Instance.PlaySound(AppSounds.PLAYER_MOV3); ///// PLACEHOLDER
                break;
            case 3:
                MusicManager.Instance.PlaySound(AppSounds.PLAYER_MOV4); ///// PLACEHOLDER
                break;
            case 4:
                MusicManager.Instance.PlaySound(AppSounds.PLAYER_MOV5); ///// PLACEHOLDER
                break;
            case 5:
                MusicManager.Instance.PlaySound(AppSounds.PLAYER_MOV6); ///// PLACEHOLDER
                break;
            case 6:
                MusicManager.Instance.PlaySound(AppSounds.PLAYER_MOV7); ///// PLACEHOLDER
                break;
            case 7:
                MusicManager.Instance.PlaySound(AppSounds.PLAYER_MOV8); ///// PLACEHOLDER
                break;
            case 8:
                MusicManager.Instance.PlaySound(AppSounds.PLAYER_MOV9); ///// PLACEHOLDER
                break;
            default:
                break;
        }
    }
}
