using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class CameraSwitcher : MonoBehaviour
{

    [Header("cámaras")]
    public List<Camera> camaras;
    private int indiceAtual = 0;

    public float min = 0f;
    public float max = 30f;
    public float camaraY = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AcivarSoloEsta(indiceAtual);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CambiarCamara()
    {
        indiceAtual++;
        if (indiceAtual == 3) indiceAtual++;
        if(indiceAtual >= camaras.Count)indiceAtual = 0;
        AcivarSoloEsta(indiceAtual);
    }

    public void OnCambioCamara(InputValue value)
    {
        if (value.isPressed)
        {
            if (!GetComponent<PlayerMovement>().isAim())
            {
                CambiarCamara();
            }
        }
    }
    public void OnApuntar(InputValue value)
    {
        if (value.isPressed)
        {
            if (GetComponent<PlayerMovement>().isAim())
            {
                AcivarSoloEsta(3);
            }
            else
            {
                AcivarSoloEsta(indiceAtual);
            }
        }
    }
    public void AcivarSoloEsta(int index)
    {
        for (int i = 0; i<camaras.Count; i++)
        {
            camaras[i].gameObject.SetActive(i == index);
            if (i == index)
            {
                camaras[i].gameObject.tag = "MainCamera";
                if(i == 0)
                {
                    min = 0f;
                    max = 30f;
                    camaraY = 0f;
                    GetComponent<PlayerLook>().camGun = false;
                }
                if (i == 1)
                {
                    min = -20f;
                    max = 20f;
                    camaraY = 0f;
                    GetComponent<PlayerLook>().camGun = false;
                }
                if (i == 2)
                {
                    min = -20f;
                    max = 20f;
                    camaraY = 180f;
                    GetComponent<PlayerLook>().camGun = false;
                }
                if (i == 3)
                {
                    min = -10f;
                    max = 20f;
                    camaraY = 0f;
                    GetComponent<PlayerLook>().camGun = true;
                }
                GetComponent<PlayerLook>().ActualizarPitch(min ,max, camaraY);
            }
            else
            {
                camaras[i].gameObject.tag = "Untagged";
            }
        }
        GetComponent<PlayerLook>().UpdateActiveCamera();
    }
}
