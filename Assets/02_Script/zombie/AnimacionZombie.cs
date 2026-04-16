using UnityEngine;

public class AnimacionZombie : MonoBehaviour
{

    [SerializeField] private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        animator.SetBool("run", gameObject.GetComponent<Hit>().IsDamage());
        if (gameObject.GetComponent<Hit>().isDead() || gameObject.GetComponentInChildren<HeadHit>().isDead())
        {
            animator.SetBool("muerto", true);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
