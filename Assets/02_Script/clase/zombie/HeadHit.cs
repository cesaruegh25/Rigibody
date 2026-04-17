using UnityEngine;

public class HeadHit : MonoBehaviour
{
    public bool muerto = false;
    public AudioSource audioSource;

    [SerializeField] private GameObject efectoSangre;
    [SerializeField] private Transform puntoSangrado;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        muerto = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool isDead()
    {
        return muerto;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bala"))
        {
            Vector3 posicion = other.gameObject.transform.position;
            GameObject sangre = Instantiate(efectoSangre, posicion, Quaternion.identity);
            sangre.transform.LookAt(GameObject.Find("Player").transform);
            sangre.transform.SetParent(transform.GetChild(0));

            Debug.Log("HeadShoot");
            muerto = true;
            audioSource.Play();
            Destroy(other.gameObject);
            Destroy(sangre, 5f);
            //cuerpo zombie
            //Destroy(gameObject.transform.parent.parent.parent.parent.parent.parent);
        }
    }
}
