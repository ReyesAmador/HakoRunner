using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    public Color defaultColor = Color.white;
    public Color highlightColor = Color.cyan;
    public float detectionDistance = 5f;

    private Image crosshairImage;

    void Start()
    {
        crosshairImage = GetComponent<Image>();
    }

    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.SphereCast(ray, 0.2f, out RaycastHit hit, detectionDistance))
        {
            if (hit.collider.CompareTag("Item"))
            {
                crosshairImage.color = highlightColor;
                return;
            }
        }

        crosshairImage.color = defaultColor;
    }
}

