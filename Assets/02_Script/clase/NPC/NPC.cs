using System.Collections;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public bool cerca = false;
    public bool interactuando = false;
    public bool tiempo = true;

    public PlayerMovement playerMovement;
    public PlayerLook playerLook;
    public PlayerInteractive playerInteractive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cerca = false;
        interactuando = false;
        tiempo = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (cerca && playerInteractive.interactuar)
        {
            interactuando = true;
            playerInteractive.interactuar = false;
        }
         if (interactuando)
        {
            playerMovement.desactivarMovimiento(true);
            //playerLook.enabled = true;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (tiempo)
            {
                cerca = true;
                tiempo = false;
                UIManager.instance.ActivateConversacion(true);
                UIManager.instance.SetConversacion("Hola, soy un NPC");

                UIManager.instance.ActivateOpciones(true);
                UIManager.instance.SetOpciones("Adiós");
                //UIManager.instance.SetOpciones("1. ¿Cómo estás? \n 2. ¿Qué haces? \n 3. Adiós");
                //playerLook.enabled = false;
                playerMovement.desactivarMovimiento(false);
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cerca = false;
            UIManager.instance.SetConversacion("Adiós, soy un NPC");
            UIManager.instance.ActivateConversacion(false);
            UIManager.instance.ActivateOpciones(false);
            interactuando = false;
            playerInteractive.interactuar = false;
            playerMovement.desactivarMovimiento(true);
            StopCoroutine("delay");
            StartCoroutine(delay());
        }
    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(5);
        tiempo = true;
    }
}
