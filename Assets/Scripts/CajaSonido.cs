using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CajaSonido : MonoBehaviour
{
    public AudioClip impactoClip; // Sonido de golpe
    [Range(0f, 1f)] public float volumen = 0.5f;
    public float fuerzaMinima = 1.0f; // Umbral para que suene

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f; // 3D
    }

    private void OnCollisionEnter(Collision collision)
    {
        float fuerzaImpacto = collision.relativeVelocity.magnitude;

        if (fuerzaImpacto >= fuerzaMinima && impactoClip != null)
        {
            audioSource.PlayOneShot(impactoClip, volumen);
        }
    }
}
