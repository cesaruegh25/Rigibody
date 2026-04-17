using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerLook : MonoBehaviour
{
    [Header("Sensitivity")]
        public float mouseSensivity = 0.15f;
    [Header("Pitch clamp")]
        private float minPitch = -70f;
        private float maxPitch = 70f;
        private float camaraY = 0f;

    private Vector2 lookInput;
    private float pitch;
    private Transform cameraTransform;
    private Camera currentCamera;
    private bool canMove = true;
    private bool muerto = false;
    public bool camGun = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateActiveCamera();
        // centra el cursor y lo pone invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {
            return;
        }
        //UpdateActiveCamera();
        if (cameraTransform == null)
        {
            return;
        }
        // Rotacion camara
        pitch -= lookInput.y * mouseSensivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);// capear el movimiento
        cameraTransform.localRotation = Quaternion.Euler(pitch, camaraY, 0);
        // Rotacion horizontal
        float yaw = lookInput.x * mouseSensivity;
        transform.Rotate(0, yaw, 0, Space.Self);// Space,Self es para que gire sobre si mismo
        if (camGun)
        {
            GameObject mano = GameObject.Find("mano");
            mano.transform.localRotation = Quaternion.Euler(mano.transform.rotation.x, mano.transform.rotation.y, mano.transform.rotation.z - pitch);
        }
    }
    public void OnLook(InputValue value)
    {
        if (gameObject.GetComponent<PlayerMovement>().muerto)return;
        lookInput = value.Get<Vector2>();
    }
    // se ejecuta antes que el start se usa para centrar la camara
    private void OnEnable()
    {
        lookInput = Vector2.zero;
    }
    public void UpdateActiveCamera()
    {
        if (Camera.main != currentCamera)
        {
            currentCamera = Camera.main;
            if (currentCamera != null)
            {
                cameraTransform = currentCamera.transform;
                pitch = cameraTransform.localEulerAngles.x;
                //if (pitch > 180) pitch -= 360;
            }
        }
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }
    public void ActualizarPitch(float min, float max, float Y)
    {
        minPitch = min;
        maxPitch = max;
        camaraY = Y;
    }
}
