using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl_MovementController : MonoBehaviour
{
    protected PlayerInputAsset actions;

    float moveHorizontal;
    float moveVertical;

    [Header("Variables de Movimiento")]
    [HideInInspector] public float controlSpeedX;
    [HideInInspector] public float controlSpeedY;
    public float maxSpeedX;
    public float maxSpeedY;
    public float accelerationX;
    public float accelerationY;
    public float decelerationX;
    public float decelerationY;
    protected Rigidbody2D controlRb;

    [Header("El Objeto que Funciona como Brazo o el que Rota")]
    public GameObject armObject;
    [HideInInspector] public Vector2 armDirection;
    protected float angle;

    // Start is called before the first frame update
    protected virtual void Start()
    {
       
        actions = new PlayerInputAsset();
        actions.PlayerInputActions.Enable();
        controlRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
   protected virtual void Update()
    {
        moveHorizontal = actions.PlayerInputActions.HorizontalMovement.ReadValue<float>();
        moveVertical = actions.PlayerInputActions.VerticalMovement.ReadValue<float>();

        PlayerControlledMovement(moveHorizontal, moveVertical);

        ControlArmRotation();

        //Seteo de variables de animaciones:
        SetAnimationsVariables(); 

    }

    protected virtual void PlayerControlledMovement(float axisValueX, float axisValueY)
    {
        controlSpeedX += axisValueX * accelerationX * Time.deltaTime; ////Cálculo de la velocidad con aceleraciones
        controlSpeedY += axisValueY * accelerationY * Time.deltaTime;
        controlRb.velocity = new Vector2(controlSpeedX, controlSpeedY); ////Aplicación de los cálculos al velocity del rigidbody

        if (axisValueX == 0 || axisValueX == 1 && controlSpeedX < 0 || axisValueX == -1 && controlSpeedX > 0) ////Deceleraciones en X
        {
            if (controlSpeedX > 0)  
            {
                controlSpeedX -= decelerationX * Time.deltaTime;
                if (controlSpeedX < 0)
                {
                    controlSpeedX = 0;
                }
            }
            else if (controlSpeedX < 0)
            {
                controlSpeedX += decelerationX * Time.deltaTime;
                if (controlSpeedX > 0)
                {
                    controlSpeedX = 0;
                }
            }
        }

        if (axisValueY == 0 || axisValueY == 1 && controlSpeedY < 0 || axisValueY == -1 && controlSpeedY > 0) ////Deceleraciones en Y
        {
            if (controlSpeedY > 0)  
            {
                controlSpeedY -= decelerationY * Time.deltaTime;
                if (controlSpeedY < 0)
                {
                    controlSpeedY = 0;
                }
            }
            else if (controlSpeedY < 0)
            {
                controlSpeedY += decelerationY * Time.deltaTime;
                if (controlSpeedY > 0)
                {
                    controlSpeedY = 0;
                }
            }
        }

        if (controlSpeedX > maxSpeedX)  ////Límite de velocidad
        {
            controlSpeedX = maxSpeedX;
        }
        if (controlSpeedX < -maxSpeedX)
        {
            controlSpeedX = -maxSpeedX;
        }
        if (controlSpeedY > maxSpeedY)
        {
            controlSpeedY = maxSpeedY;
        }
        if (controlSpeedY < -maxSpeedY)
        {
            controlSpeedY = -maxSpeedY;
        }
    }

    protected virtual void ControlArmRotation()
    {
       
        armDirection = actions.PlayerInputActions.Rotating.ReadValue<Vector2>();  ////La dirección del joystick de rotación, el derecho

        if (armDirection.sqrMagnitude > 0) ////Si el valor de la dirección es mayor que 0...
        {
            angle = Mathf.Atan2(armDirection.normalized.y, armDirection.normalized.x) * Mathf.Rad2Deg; ////Se calcula el ángulo entre en eje X y el vector de direccion del joystick
            armObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); ////Se rota el objeto de brazo para igualar la dirección del joystick en el eje Z.
        }

        else if (controlRb.velocity.normalized.magnitude != 0) //Si no se controla la dirección y se está moviendo
        {
            angle = Vector2.SignedAngle(Vector2.right, controlRb.velocity.normalized);
            armObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); ////Se rota el objeto de brazo para igualar la dirección del joystick en el eje Z.
        }

        else //Si NO controla la dirección y no se mueve mantiene el ángulo anterior
        {
            armObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    ///////////////////////////////////////////////ANIMACIONES//////////////////////////////////////////////////

    [Header("Animators")]

    [SerializeField] protected Animator m_characterAnimator;

    protected virtual void SetAnimationsVariables() //Solo funciona en player
    {
        m_characterAnimator.SetBool("IsMoving", controlRb.velocity.magnitude > 0.1f|| controlRb.velocity.magnitude < -0.1f);
        m_characterAnimator.SetFloat("VelocityX", controlRb.velocity.normalized.x);
        m_characterAnimator.SetFloat("VelocityY", controlRb.velocity.normalized.y);
    }
}
