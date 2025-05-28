using UnityEngine;

public class Caja : MonoBehaviour
{
    public int peso = 1;  // Valor por defecto, puede ser 1 para madera, 2 para metal

    [Tooltip("Prefab del sistema de partículas de polvo")]
    public GameObject particulaPolvo;

    // Este valor controla cuán fuerte debe ser el impacto para que se genere el polvo
    public float umbralImpacto = 1f;

    private void OnCollisionEnter(Collision collision)
    {
        // Solo instanciar partículas si el impacto es suficientemente fuerte
        if (collision.relativeVelocity.magnitude >= umbralImpacto && particulaPolvo != null)
        {
            // Obtener el punto y la dirección del primer contacto
            ContactPoint contacto = collision.contacts[0];
            Vector3 puntoImpacto = contacto.point;
            Quaternion rotacion = Quaternion.LookRotation(contacto.normal);

            // Instanciar el prefab y reproducir el sistema de partículas
            GameObject efecto = Instantiate(particulaPolvo, puntoImpacto, rotacion);
            ParticleSystem ps = efecto.GetComponent<ParticleSystem>();

            if (ps != null)
                ps.Play();

            // Destruir el efecto después de 2 segundos para no llenar la escena de partículas
            Destroy(efecto, 2f);
        }
    }
}

