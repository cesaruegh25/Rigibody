using UnityEngine;
using UnityEngine.InputSystem;

public class Disparar : MonoBehaviour
{

    [Header("Bala")]
        [SerializeField] private GameObject prefabBala;

    [Header("Punto de disparo")]
        public Transform puntoDisparo;
    
    public AnimacionesPlayer animator;

    private void Disparado()
    {
        if (prefabBala == null || puntoDisparo == null) return;

        GameObject bala = Instantiate(prefabBala, puntoDisparo.position, puntoDisparo.rotation);
    }

    public void OnDisparar(InputValue value)
    {
        if (value.isPressed)
        {
            if (GetComponent<PlayerMovement>().IsGrounded() && GetComponent<PlayerMovement>().isAim())
            {
                animator.AnimacionDisparar();
            }
            if (GetComponent<PlayerMovement>().IsGrounded() && !GetComponent<PlayerMovement>().isAim() && !animator.puedoGolpear)
            {
                animator.AnimacionGolpear();
            }
        }
    }
}
