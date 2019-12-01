using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOrderInLayer : MonoBehaviour
{
    [SerializeField] Transform transformRef;
    SpriteRenderer mySprite;
    private void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        mySprite.sortingOrder = -Mathf.RoundToInt(transformRef.position.y*100f);
    }
}
