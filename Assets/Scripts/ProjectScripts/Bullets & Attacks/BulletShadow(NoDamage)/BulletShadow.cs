using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShadow : MonoBehaviour
{
    [Header("Transform padre (la bala), posición local en Y de la sombra y su collider")]
    private Transform bulletTransform;
    private float initialYPosition;
    private Collider2D shadowCollider;

    [Header("El ángulo en Radianes")]
    private float angleRadians;

    // Start is called before the first frame update
    void Start()
    {
        bulletTransform = transform.root.GetComponent<Transform>(); //Coger transform del padre
        initialYPosition = transform.localPosition.y;
        shadowCollider = GetComponent<Collider2D>();

        CalculateSeparationFromBulletBody();
        CalculateColliderPosition();
    }

    void CalculateSeparationFromBulletBody ()
    {
        angleRadians = bulletTransform.eulerAngles.z * Mathf.Deg2Rad; //Se pasa el ángulo de la rotación en z de la bala padre a radianes para poder usar Mathf.Cos sin errores
        transform.localPosition = new Vector2(0, initialYPosition * Mathf.Cos(angleRadians)); //Se pone la posición local de la sombra en un valor en Y resultado de la multiplicación de la Y inicial por el coseno del ángulo de rotación.
    }                                                                                         //De esta manera, el valor mínimo en Y es 0 y el máximo es la posición incial.

    void CalculateColliderPosition()  //Se coloca el collider en un punto medio entre la sombra y la bala.
    {
        if (bulletTransform.eulerAngles.z <= 90 || bulletTransform.eulerAngles.z >= 270) //Se coloca el valor positivo en Y
        {
            shadowCollider.offset = new Vector2(shadowCollider.offset.x, Mathf.Abs((bulletTransform.position.y - transform.position.y) / 2));
        }
        else if (bulletTransform.eulerAngles.z > 90 || bulletTransform.eulerAngles.z < 270) //Se coloca el valor negativo en Y, porque la bala se instancia boca abajo por la rotación
        {
            shadowCollider.offset = new Vector2(shadowCollider.offset.x, -Mathf.Abs((bulletTransform.position.y - transform.position.y) / 2));
        }
    }
}