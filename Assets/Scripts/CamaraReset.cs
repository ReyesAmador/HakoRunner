using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class CamaraReset : MonoBehaviour
{
    private CinemachineDeoccluder camCollider;

    void Awake()
    {
        camCollider = GetComponent<CinemachineDeoccluder>();
    }

    void Start()
    {
        StartCoroutine(ActivarColliderConDelay());
    }

    IEnumerator ActivarColliderConDelay()
    {
        if (camCollider != null)
        {
            yield return new WaitForSeconds(0.3f);
            camCollider.enabled = true;
        }
    }
}
