using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarCamara : MonoBehaviour
{
    public Camera primeraCamara;
    public Camera segundaCamara;

    void Start()
    {
        // Desactiva la segunda c�mara al inicio
        segundaCamara.enabled = false;
    }

    void Update()
    {
        // Cambia entre c�maras al presionar la tecla "G"
        if (Input.GetKeyDown(KeyCode.G))
        {
            primeraCamara.enabled = !primeraCamara.enabled;
            segundaCamara.enabled = !segundaCamara.enabled;
        }
    }
}