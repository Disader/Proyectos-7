using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GroundEffect", menuName = "GroundEffect")]
public class GroundEffect_SO : ScriptableObject
{
    [SerializeField]
    public Sprite sprite;
    [SerializeField]
    public AnimationClip animation;
    [SerializeField]
    public bool isFire;
    [SerializeField]
    public int touchDamage;
    [SerializeField]
    public int damageByTime;
    [SerializeField]
    public float timeInFire;
}
