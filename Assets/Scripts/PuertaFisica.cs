using UnityEngine;

public class PuertaFisica : MonoBehaviour
{
    private Rigidbody rb;
    private HingeJoint hinge;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        hinge = GetComponent<HingeJoint>();

        // La puerta está bloqueada al inicio
        if (rb != null)
            rb.isKinematic = true;

        if (hinge != null)
        {
            hinge.useSpring = false; // no puede moverse
            hinge.useLimits = true;

            JointLimits limits = new JointLimits
            {
                min = 0,
                max = 0 // sin movimiento
            };
            hinge.limits = limits;
        }
    }

    // Llamado desde la báscula cuando el peso es correcto
    public void DesbloquearPuerta()
    {

        Debug.Log("→ DesbloquearPuerta() llamado - Rigidbody: " + rb + ", HingeJoint: " + hinge);


        if (rb != null)
            rb.isKinematic = false;

        if (hinge != null)
        {
            JointSpring spring = new JointSpring
            {
                spring = 20f,
                damper = 2f,
                targetPosition = 0f
            };
            hinge.spring = spring;
            hinge.useSpring = true;

            JointLimits limits = new JointLimits
            {
                min = -90f,
                max = 90f
            };
            hinge.limits = limits;
            hinge.useLimits = true;
        }

        Debug.Log("Puerta desbloqueada");
        Debug.Log("Estado final de isKinematic: " + rb.isKinematic);
    }

    public void BloquearPuerta()
    {
        if (rb != null)
            rb.isKinematic = true;

        if (hinge != null)
        {
            hinge.useSpring = false;
            JointLimits limits = new JointLimits
            {
                min = 0,
                max = 0
            };
            hinge.limits = limits;
            hinge.useLimits = true;
        }

        Debug.Log("Puerta bloqueada");
    }
}
