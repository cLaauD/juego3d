using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camara : MonoBehaviour
{
    public Transform cuadrado;
    public Camera camaraPrincipal; // Asigna la c�mara principal en el Inspector
    public Camera camaraSecundaria; // Asigna la otra c�mara en el Inspector
    public float smoothSpeed = 3.0f;
    private Vector3 offset;
    private bool isMainCamera = true; // Variable para rastrear la c�mara actual

    void Start()
    {
        offset = transform.position - cuadrado.position;

        // Aseg�rate de que la c�mara principal est� habilitada al inicio
        camaraPrincipal.enabled = true;
        camaraSecundaria.enabled = false;
    }

    void Update()
    {
        if (cuadrado != null) // Verifica si el cuadrado no es nulo
        {
            Vector3 desiredPosition = cuadrado.position + offset;

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;

            transform.LookAt(cuadrado);
        }

        // Cambia entre c�maras al presionar la tecla "G"
        if (Input.GetKeyDown(KeyCode.G))
        {
            isMainCamera = !isMainCamera;
            camaraPrincipal.enabled = isMainCamera;
            camaraSecundaria.enabled = !isMainCamera;
        }
    }
}