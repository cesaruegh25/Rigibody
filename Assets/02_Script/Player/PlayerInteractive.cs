using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractive : MonoBehaviour
{
    public bool interactuar = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.fKey.isPressed)
        {
            interactuar = true;
            //Debug.LogWarning("Interactuar " + interactuar);
        }
    }
}
