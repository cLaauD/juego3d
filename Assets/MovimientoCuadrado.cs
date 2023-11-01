using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimientoCuadrado : MonoBehaviour
{
    public float velocidadMovimiento = 5.0f;
    public float fuerzaSalto = 10f;
    public Color newColor = Color.yellow;
    private Color colorOriginal;
    private int capsulasRecogidas = 0;
    public int totalCapsulas = 8;
    private bool mostrarMensaje = true;
    private AudioSource sonidoCapsula; // Audio para el sonido del diamante
    public ParticleSystem particleSystem; // Agrega aquí tu sistema de partículas

    void Start()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        colorOriginal = GetComponent<Renderer>().material.color;
        Debug.Log("Presiona 'G' para cambiar de cámara");
        sonidoCapsula = GetComponent<AudioSource>(); // Audio al objeto del jugador
        sonidoCapsula.playOnAwake = false;
    }

    void Update()
    {
        float movimientoHorizontal = Input.GetAxis("Vertical") * velocidadMovimiento * Time.deltaTime;
        transform.Translate(Vector3.left * movimientoHorizontal);

        float movimientoVertical = Input.GetAxis("Horizontal") * velocidadMovimiento * Time.deltaTime;
        transform.Translate(Vector3.forward * movimientoVertical);

        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Capsula"))
        {
            Debug.Log("Monedita");
            Destroy(collision.gameObject);
            GetComponent<Renderer>().material.color = newColor;
            StartCoroutine(RevertirColorDespuesDeTiempo());
            capsulasRecogidas++;
            Debug.Log("Cápsula recogida. Total: " + capsulasRecogidas);
            if (sonidoCapsula != null)
            {
                sonidoCapsula.Play();
            }

            // Activa el sistema de partículas asociado a la cápsula que recolectaste
            ParticleSystem capsuleParticleSystem = collision.gameObject.GetComponent<ParticleSystem>();
            if (capsuleParticleSystem != null)
            {
                capsuleParticleSystem.Play();
            }

            if (capsulasRecogidas == totalCapsulas)
            {
                StartCoroutine(ReiniciarJuegoDespuesDeTiempo());
            }
        }
    }

    IEnumerator RevertirColorDespuesDeTiempo()
    {
        yield return new WaitForSeconds(1.0f);
        GetComponent<Renderer>().material.color = colorOriginal;
    }

    IEnumerator ReiniciarJuegoDespuesDeTiempo()
    {
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Ganaste!!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}