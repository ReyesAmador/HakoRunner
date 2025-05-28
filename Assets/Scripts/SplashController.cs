using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashController : MonoBehaviour
{

    public Image logoImage;
    public GameObject textoPulsaBoton;

    private float timer = 0f;
    private bool puedeSalir = false;
    private float fadeDuration = 1.5f;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Comienza con el texto oculto
        textoPulsaBoton.SetActive(false);

        // Inicia el logo transparente
        Color color = logoImage.color;
        color.a = 0f;
        logoImage.color = color;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Fade-in del logo
        if (timer <= fadeDuration)
        {
            Color color = logoImage.color;
            color.a = Mathf.Clamp01(timer / fadeDuration);
            logoImage.color = color;
        }

        // Mostrar mensaje después de 2s
        if (timer >= 2f && !puedeSalir)
        {
            textoPulsaBoton.SetActive(true);
            puedeSalir = true;
        }

        if (puedeSalir && Input.anyKeyDown)
        {
            SceneManager.LoadScene("MainMenu"); // Asegúrate de que esté bien escrito
        }
    }
}
