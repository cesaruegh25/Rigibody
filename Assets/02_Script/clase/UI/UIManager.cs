using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] TextMeshProUGUI opciones;
    [SerializeField] TextMeshProUGUI conversacion;
    [SerializeField] Button boton;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boton.gameObject.SetActive(false);
        conversacion.gameObject.SetActive(false);
        opciones.gameObject.SetActive(false);
        StopCoroutine("delay");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActivateOpciones(bool active)
    {
        if (!active)
        {
            opciones.gameObject.SetActive(false);
            boton.gameObject.SetActive(false);
        }
        else
        {
            boton.gameObject.SetActive(active);
            opciones.gameObject.SetActive(active);
        }
    }
    public void ActivateConversacion(bool active)
    {
        if (!active)
        {
            conversacion.gameObject.SetActive(false);
        }
        else
        {
            conversacion.gameObject.SetActive(active);
        }
    }
    public void SetOpciones(string texto)
    {
        opciones.text = texto;
    }
    public void SetConversacion(string texto)
    {
        conversacion.text = texto;
    }
}
