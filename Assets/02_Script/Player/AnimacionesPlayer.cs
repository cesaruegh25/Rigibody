using UnityEngine;

public class AnimacionesPlayer : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;

    public bool puedoGolpear = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        puedoGolpear = false;
    }
    public void AnimacionSaltar1()
    {
        animator.SetTrigger("Saltar");
    }
    public void AnimcionSaltar2()
    {
        animator.SetTrigger("Saltar2");
    }
    public void AnimationSuelo()
    {
        animator.SetTrigger("Caida");
    }
    public void AnimacionDisparar()
    {
        animator.SetTrigger("Disparo");
    }
    public void AnimacionGolpear()
    {
        animator.SetTrigger("Golpeo");
    }
    public void AnimationGolpeHecho()
    {
        animator.SetTrigger("GolpeHecho");
    }
    public void PuedoGolpear()
    {
        puedoGolpear = true;
    }
    public void NoPuedoDisparar()
    {
        puedoGolpear = false;
    }
    private void FixedUpdate()
    {
        Vector3 vWorrld = rb.linearVelocity;
        Vector3 vLocal = transform.InverseTransformDirection(vWorrld);
        animator.SetFloat("X",vLocal.x);
        animator.SetFloat("Y", vLocal.z);
        animator.SetFloat("Z", vLocal.y);
        animator.SetBool("Ground", gameObject.GetComponent<PlayerMovement>().IsGrounded());
        animator.SetBool("back", gameObject.GetComponent<PlayerMovement>().BackMove());
        animator.SetBool("Crouch", gameObject.GetComponent<PlayerMovement>().IsCrouch());
        animator.SetBool("Apuntar", gameObject.GetComponent<PlayerMovement>().isAim());
        animator.SetBool("Golpe", puedoGolpear);


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
