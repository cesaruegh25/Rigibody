using UnityEngine;

public class Bomb : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.rotation = Quaternion.Euler(180f, 0, 0);
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
