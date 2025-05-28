using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class DetectorBascula : MonoBehaviour
{
    public int pesoObjetivo = 3;
    private int pesoActual = 0;
    private List<Caja> cajas = new List<Caja>();

    [Header("Grupo de luces/emergencia")]
    public List<Light> luces;
    public List<RotarLuz> rotadores;

    [Header("UI")]
    public TextMeshPro textoPeso;

    [Header("Puerta")]
    public PuertaFisica puerta;

    [SerializeField] private AudioSource sirenaSource;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Algo ha entrado: " + other.name);

        Caja caja = other.GetComponent<Caja>();
        if (caja != null && !cajas.Contains(caja))
        {
            cajas.Add(caja);
            pesoActual += caja.peso;
            Debug.Log("Peso actual: " + pesoActual);
            ActualizarTexto();
            VerificarPeso();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Caja caja = other.GetComponent<Caja>();
        if (caja != null && cajas.Contains(caja))
        {
            cajas.Remove(caja);
            pesoActual -= caja.peso;
            Debug.Log("Peso actual: " + pesoActual);
            ActualizarTexto();
            VerificarPeso();
        }
    }

    private void ActualizarTexto()
    {
        if (textoPeso != null)
        {
            textoPeso.text = $"Peso:{pesoActual}";
        }
    }

    private void VerificarPeso()
    {
        if (pesoActual == pesoObjetivo)
        {
            Debug.Log("¡Peso correcto!");
            ActivarLuz(true);
            ActivarSirena();

            if (puerta != null)
            {
                puerta.DesbloquearPuerta();
            }
        }
        else
        {
            Debug.Log("Peso incorrecto.");
            ActivarLuz(false);
            DesactivarSirena();
            if (puerta != null)
            {
                puerta.BloquearPuerta();
            }
        }
    }

    private void ActivarLuz(bool estado)
    {
        foreach (var luz in luces)
        {
            if (luz != null) luz.enabled = estado;
        }

        foreach (var rotador in rotadores)
        {
            if (rotador != null) rotador.enabled = estado;
        }
    }

    private void ActivarSirena()
    {
        if (!sirenaSource.isPlaying)
        {
            sirenaSource.Play();
        }
    }
    
    private void DesactivarSirena()
    {
        if (sirenaSource.isPlaying)
            sirenaSource.Stop();
    }
}

