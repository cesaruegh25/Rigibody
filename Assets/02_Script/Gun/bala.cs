using UnityEngine;

public class Bala : MonoBehaviour
{

    public float speed = 20f;
    public float tiempoVida = 4f;
    private Rigidbody rb;


    void Start()
    {
        Destroy(gameObject, tiempoVida);
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * speed;
    }

    void Update()
    {
        
    }
}
