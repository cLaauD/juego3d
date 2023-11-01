using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Espera : MonoBehaviour
{
    public Transform cuadrado; // Referencia al transform del cuadrado
    public float velocidad = 10f;

    void Update()
    {
        if (cuadrado != null) // Comprueba si el cuadrado aún existe
        {
            Vector3 direccion = cuadrado.position - transform.position;
            float distancia = direccion.magnitude;

            if (distancia > 0.1f) // Ajusta el valor límite según lo necesites
            {
                direccion.Normalize();
                transform.rotation = Quaternion.LookRotation(direccion);
                transform.Translate(direccion * velocidad * Time.deltaTime, Space.World);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cuadrado"))
        {
            Debug.Log("¡Perdiste! Reiniciando juego..."); // Mensaje en la consola
            Destroy(collision.gameObject); // Destruye el cuadrado al chocar
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}