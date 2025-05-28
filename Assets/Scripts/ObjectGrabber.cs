using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ObjectGrabber : MonoBehaviour
{
    public float grabDistance = 3f;              // Distancia máxima para coger el objeto
    public Transform holdPoint;                 // Punto donde se mantendrá el objeto
    private GameObject objetoActual = null;
    private Rigidbody objetoRb = null;
    private ConfigurableJoint joint = null;
    private Camera mainCamera;
    private float cooldownRecoger = 0f;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {

        if (cooldownRecoger > 0f)
        {
            cooldownRecoger -= Time.deltaTime;
        }

        if (Keyboard.current.eKey.wasPressedThisFrame && cooldownRecoger <= 0f)
        {
            Debug.Log("E fue pulsada");
            if (objetoActual == null)
            {
                TryPickUpObject();
            }
            else
            {
                DropObject();
            }
        }

        if (Mouse.current.leftButton.wasPressedThisFrame && objetoActual != null)
        {
            DispararObjeto();
        }
    }

    void FixedUpdate()
    {
        if (joint != null && objetoActual != null)
        {
            joint.connectedAnchor = holdPoint.position;
        }
    }

    void TryPickUpObject()
    {
        Vector3 rayOrigin = Camera.main.transform.position + Vector3.down * 0.3f;
        Ray ray = new Ray(rayOrigin, Camera.main.transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * grabDistance, Color.cyan, 1.5f);


        if (Physics.SphereCast(ray, 0.5f, out RaycastHit hit, grabDistance))
        {
            Debug.Log("Hit: " + hit.collider.name);

            // Si el jugador mira al botón de victoria
            if (hit.collider.CompareTag("Interactuable") && hit.collider.name == "BotonVictoria")
            {
                Debug.Log("Botón de victoria activado");

                // Parar el cronómetro
                Crono.instance.PararCronometro();

                // Guardar el tiempo final
                PlayerPrefs.SetFloat("TiempoFinal", Crono.instance.TiempoFinal);

                // Cargar la pantalla de victoria
                SceneManager.LoadScene("PantallaVictoria");
                return;
            }

            if (hit.collider.CompareTag("Item"))
            {
                objetoActual = hit.collider.gameObject;
                objetoRb = objetoActual.GetComponent<Rigidbody>();

                // Añadimos el joint configurable
                joint = objetoActual.AddComponent<ConfigurableJoint>();
                joint.connectedBody = null; // No conectamos a un rigidbody, sino a un punto

                // Orientar el objeto hacia la dirección de la cámara
                Quaternion rotacionObjetivo = Quaternion.LookRotation(Camera.main.transform.forward);
                objetoActual.transform.rotation = rotacionObjetivo;

                joint.anchor = Vector3.zero;
                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = holdPoint.position;

                // Movimiento libre, pero con resorte hacia el punto
                joint.xMotion = ConfigurableJointMotion.Limited;
                joint.yMotion = ConfigurableJointMotion.Limited;
                joint.zMotion = ConfigurableJointMotion.Limited;

                JointDrive drive = new JointDrive
                {
                    positionSpring = 2000f,
                    positionDamper = 80f,
                    maximumForce = 10000f
                };

                joint.xDrive = drive;
                joint.yDrive = drive;
                joint.zDrive = drive;

                // Rotación libre o limitada según prefieras
                joint.angularXMotion = ConfigurableJointMotion.Locked;
                joint.angularYMotion = ConfigurableJointMotion.Locked;
                joint.angularZMotion = ConfigurableJointMotion.Locked;
            }
        }
        else
        {
            Debug.Log("Raycast no ha tocado nada");
        }
    }

    void DropObject()
    {
        if (objetoActual != null)
        {
            var joint = objetoActual.GetComponent<ConfigurableJoint>();
            if (joint != null)
            {
                Destroy(joint);
            }

            objetoRb = null;
            objetoActual = null;
        }
    }

    void DispararObjeto()
    {
        if (objetoActual != null)
        {
            // Quitar el joint si lo tiene
            var joint = objetoActual.GetComponent<ConfigurableJoint>();
            if (joint != null)
            {
                Destroy(joint);
            }

            // Asegurar que tiene Rigidbody y que no esté congelado
            Rigidbody rb = objetoActual.GetComponent<Rigidbody>();
            rb.freezeRotation = false;

            // Aplicar fuerza hacia delante desde la cámara
            float fuerzaDisparo = 1200f;
            rb.AddForce(Camera.main.transform.forward * fuerzaDisparo);

            // Limpiar referencias
            objetoActual = null;
            objetoRb = null;

            cooldownRecoger = 0.5f;
        }
    }

}
