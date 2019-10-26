using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl_MovementController : MonoBehaviour
{
    float moveHorizontal;
    float moveVertical;

    [Header("Variables de Movimiento")]
    [HideInInspector]
    public float playerSpeedX;
    [HideInInspector]
    public float playerSpeedY;
    public float maxSpeedX;
    public float maxSpeedY;
    public float accelerationX;
    public float accelerationY;
    public float decelerationX;
    public float decelerationY;
    private Rigidbody2D playerRb;

    [Header("El Objeto que Funciona como Brazo o el que Rota")]
    public GameObject armObject;
    [HideInInspector] public Vector2 armDirection;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        PlayerControlledMovement(moveHorizontal, moveVertical);

        ControlArmRotation();
    }

    void PlayerControlledMovement(float axisValueX, float axisValueY)
    {
        playerSpeedX += axisValueX * accelerationX * Time.deltaTime; ////Cálculo de la velocidad con aceleraciones
        playerSpeedY += axisValueY * accelerationY * Time.deltaTime;
        playerRb.velocity = new Vector2(playerSpeedX, playerSpeedY); ////Aplicación de los cálculos al velocity del rigidbody

        if (axisValueX == 0 || axisValueX == 1 && playerSpeedX < 0 || axisValueX == -1 && playerSpeedX > 0) ////Deceleraciones en X
        {
            if (playerSpeedX > 0)  
            {
                playerSpeedX -= decelerationX * Time.deltaTime;
                if (playerSpeedX < 0)
                {
                    playerSpeedX = 0;
                }
            }
            else if (playerSpeedX < 0)
            {
                playerSpeedX += decelerationX * Time.deltaTime;
                if (playerSpeedX > 0)
                {
                    playerSpeedX = 0;
                }
            }
        }

        if (axisValueY == 0 || axisValueY == 1 && playerSpeedY < 0 || axisValueY == -1 && playerSpeedY > 0) ////Deceleraciones en Y
        {
            if (playerSpeedY > 0)  
            {
                playerSpeedY -= decelerationY * Time.deltaTime;
                if (playerSpeedY < 0)
                {
                    playerSpeedY = 0;
                }
            }
            else if (playerSpeedY < 0)
            {
                playerSpeedY += decelerationY * Time.deltaTime;
                if (playerSpeedY > 0)
                {
                    playerSpeedY = 0;
                }
            }
        }

        if (playerSpeedX > maxSpeedX)  ////Límite de velocidad
        {
            playerSpeedX = maxSpeedX;
        }
        if (playerSpeedX < -maxSpeedX)
        {
            playerSpeedX = -maxSpeedX;
        }
        if (playerSpeedY > maxSpeedY)
        {
            playerSpeedY = maxSpeedY;
        }
        if (playerSpeedY < -maxSpeedY)
        {
            playerSpeedY = -maxSpeedY;
        }
    }

    void ControlArmRotation()
    {
        armDirection = Vector2.right * Input.GetAxisRaw("RotatingX") + Vector2.up * Input.GetAxisRaw("RotatingY"); ////La dirección del joystick de rotación, el derecho

        if (armDirection.sqrMagnitude > 0) ////Si el valor de la dirección es mayor que 0...
        {
            angle = Mathf.Atan2(armDirection.normalized.y, armDirection.normalized.x) * Mathf.Rad2Deg; ////Se calcula el ángulo entre en eje X y el vector de direccion del joystick
            armObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); ////Se rota el objeto de brazo para igualar la dirección del joystick en el eje Z.
        }
    }
}
