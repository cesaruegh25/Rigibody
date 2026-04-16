using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public AudioSource audioSource;
    public bool hit = false;
    public bool muerto = false;

    AnimacionesPlayer animacionesPlayer;

    [SerializeField] private GameObject efectoSangre;
    [SerializeField] private Transform puntoSangrado;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        muerto = false;
        animacionesPlayer = GameObject.Find("Player").GetComponent<AnimacionesPlayer>();
        hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hit)
        {
            transform.LookAt(GameObject.Find("Player").transform);
            StopCoroutine("Regeneracion");
            StartCoroutine(Regeneracion());
        }
    }
    public bool IsDamage()
    {
        return hit;
    }
    public bool isDead()
    {
        return muerto;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bala"))
        {
            if (hit)
            {
                muerto = true;
            }
            // si fuera con collision, se pondria Vector3 puntoImpacto = other.contacts[0].point;
            Vector3 posicion = other.gameObject.transform.position;
            GameObject sangre = Instantiate(efectoSangre, posicion, Quaternion.identity);
            sangre.transform.LookAt(GameObject.Find("Player").transform);
            sangre.transform.SetParent(transform.GetChild(0));
            
            Debug.Log("Hit");
            hit = true;
            audioSource.Play();
            Destroy(other.gameObject);
            Destroy(sangre, 5f);
        }
        if (other.gameObject.CompareTag("punch") && animacionesPlayer.puedoGolpear)
        {
            if (hit)
            {
                muerto = true;
            }
            Debug.Log("puñetazo");
            hit = true;
            audioSource.Play();
        }
    }
    IEnumerator Regeneracion()
    {
        yield return new WaitForSeconds(5f);
        hit = false;
    }
}
