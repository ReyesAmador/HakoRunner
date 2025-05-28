using UnityEngine;

public class RotarLuz : MonoBehaviour
{
    public float velocidadRotacion = 100f;

    void Update()
    {
        transform.Rotate(Vector3.right, velocidadRotacion * Time.deltaTime);
    }
}
