using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{

    public GameObject panelAjustes;
    public Slider sliderVolumen;


    private void Start()
    {

        // Mostrar y desbloquear el cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Inicializar volumen con el valor actual
        sliderVolumen.value = MusicManager.Instance.GetVolume();

        // Suscribirse al evento del slider
        sliderVolumen.onValueChanged.AddListener(SetVolume);
    }

    public void Jugar()
    {
        MusicManager.Instance.PlayGameplayMusic();
        SceneManager.LoadScene("Nivel1");
    }

    public void Ajustes()
    {
        panelAjustes.SetActive(true);
    }

    public void CerrarAjustes()
    {
        panelAjustes.SetActive(false);
    }

    private void SetVolume(float volumen)
    {
        Debug.Log("🎚️ Slider volumen cambiado a: " + volumen);
        MusicManager.Instance.SetVolume(volumen);
    }

    public void Salir()
    {
        Debug.Log("Salir del juego");
        Application.Quit();
    }
}
