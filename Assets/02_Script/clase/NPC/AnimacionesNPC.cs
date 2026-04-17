using UnityEngine;

public class AnimacionesNPC : MonoBehaviour
{
    [SerializeField] private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        if (gameObject.GetComponent<NPC>().cerca)
        {
            animator.SetBool("Cerca", true);
        }
        else
        {
            animator.SetBool("Cerca", false);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
