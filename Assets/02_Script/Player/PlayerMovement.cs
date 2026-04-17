using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public bool muerto = false;
    [Header("Movimiento")]
        public float speed = 5f;
        public float jumpForce = 6f;

    private Rigidbody rb;
    private Vector2 moveInput;
    public bool movimiento = true;
    [SerializeField] private float runMultiplier = 2f;
    private bool isRunning = false;
    private bool backMove = false;
    private bool isCrouch = false;
    private bool aim = false;
    [SerializeField] private float crouchMultipler = 0.5f;

    [Header("GroundCheck")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.25f;

    public AudioSource audioSource;

    public GameObject mano;
    public GameObject espalda;
    public GameObject arma;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        if (moveInput.y == -1.0f)
        {
            backMove = true;
        }
        else
        {
            backMove = false;
        }
    }
    //Update para cosas de teclado
    private void Update()
    {
        ComprobarCtrl();
        ComprobarShift();
        ComprobarAlt();
    }
    public void OnApuntar(InputValue value)
    {
        if (value.isPressed)
        {
            aim = !aim;
            Debug.Log("apuntando " + aim);
            PosicionArma(aim);
        }
    }

    private void PosicionArma(bool aim)
    {
        if (aim)
        {
            arma.transform.SetParent(mano.transform);
            arma.transform.localPosition = new Vector3(0.123263068f, 0.105208822f, 0.0104711875f);
            arma.transform.localEulerAngles = new Vector3(283.302307f, 177.245956f, 272.534607f);
        }
        else
        {
            arma.transform.SetParent(espalda.transform);
            arma.transform.localPosition = new Vector3(-0.246016368f, 0.249748573f, -0.180457756f);
            arma.transform.localEulerAngles = new Vector3(81.8805695f, 196.529495f, 102.307167f);
        }
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded) 
        {
            //Debug.Log("boton presionado");
            GetComponent<AnimacionesPlayer>().AnimacionSaltar1();
        }
    }
    public void saltar()
    {
        if (movimiento)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    // crea una esfera en el object empty que he puesto en los pies y segun el radio. detecta con que toca los pies y cambia el isGrounded.(asi no hay que poner tag ground en el suelo, ni tejados etc)
    public void GroundCheck()
    {
        Collider[] hits = Physics.OverlapSphere(groundCheck.position, groundRadius);
        bool grounded = false;
        foreach (Collider col in hits)
        {
            // esto es para que no detecte al player i cambie sin tocar el suelo.
            if (col.gameObject != gameObject)
            {
                grounded = true;
                //Debug.Log("toco: " + col.gameObject.name);
                break;
            }
        }
        if (grounded != isGrounded)
        {
            isGrounded = grounded;
        }
    }
    //fixedUpdate para las fisicas
    private void FixedUpdate()
    {
        GroundCheck();
        Vector3 direccion = transform.TransformDirection(
            new Vector3(moveInput.x, 0, moveInput.y));
        float currentSpeed = speed;
        if (isRunning)
        {
            currentSpeed = speed * runMultiplier;
        }
        if (isCrouch)
        {
            currentSpeed = speed * crouchMultipler;
        }
            
        Vector3 velocity = direccion * currentSpeed;
        Vector3 newVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
        /*if (rb.linearVelocity.y < 0.0f)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        //Debug.Log("rb.y: "+ rb.linearVelocity.y);*/
        if (movimiento)
        {
            rb.linearVelocity = newVelocity;
        

            if (moveInput.x != 0 || moveInput.y != 0 && isGrounded)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                    Debug.Log("sonido de pasos");
                }
            }
            else
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                    Debug.Log("parar sonido de pasos");
                }
            }
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ground")
        {
            Debug.Log("toco suelo con trigger");
        }
        
    }*/
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bomb")
        {
            GetComponent<AnimacionesPlayer>().AnimacionMuerto();
            movimiento = false;
            muerto = true;
        }
    }
    public bool IsGrounded()
    {
        return isGrounded;
    }
    public bool BackMove()
    {
        return backMove;
    }
    public bool IsCrouch()
    {
        return isCrouch;
    }
    public bool isAim()
    {
        return aim;
    }
    public void ComprobarCtrl()
    {
        bool ctrlPressed = Keyboard.current != null &&
            (Keyboard.current.rightCtrlKey.isPressed ||
            Keyboard.current.leftCtrlKey.isPressed);
        if (ctrlPressed)
        {
            isCrouch = true;
        }
        else
        {
            isCrouch = false;
        }
    }
    public void ComprobarAlt()
    {
        bool altPressed = Keyboard.current != null &&
            (Keyboard.current.leftAltKey.isPressed ||
            Keyboard.current.rightAltKey.isPressed);
        if (altPressed || muerto)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public void ComprobarShift()
    {
        bool shiftPressed = Keyboard.current != null &&
            (Keyboard.current.leftShiftKey.isPressed ||
            Keyboard.current.rightShiftKey.isPressed);
        if (shiftPressed)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }
    
    public void desactivarMovimiento(bool move)
    {
        movimiento = move;
    }
}
