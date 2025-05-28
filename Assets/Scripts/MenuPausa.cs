using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuUI;
    public StarterAssets.StarterAssetsInputs inputJugador;
    private bool estaPausado = false;

    public GameObject panelAjustes;
    public Slider sliderVolumen;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (estaPausado)
                ReanudarJuego();
            else
                PausarJuego();
        }
    }

    public void ReanudarJuego()
    {
        menuUI.SetActive(false);
        inputJugador.cursorInputForLook = true;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        estaPausado = false;
    }

    void PausarJuego()
    {
        menuUI.SetActive(true);
        inputJugador.cursorInputForLook = false;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        estaPausado = true;
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1f;
        int nivel = PlayerPrefs.GetInt("NivelActual", 1);
        SceneManager.LoadScene($"Nivel{nivel}");
    }

    public void AbrirAjustes()
    {
        panelAjustes.SetActive(true);
        sliderVolumen.value = MusicManager.Instance.GetVolume();
        sliderVolumen.onValueChanged.RemoveAllListeners();
        sliderVolumen.onValueChanged.AddListener(SetVolume);
    }

    public void CerrarAjustes()
    {
        panelAjustes.SetActive(false);
    }

    public void VolverAlMenu()
    {
        Time.timeScale = 1f;
        PlayerPrefs.DeleteAll();
        MusicManager.Instance.PlayMenuMusic();
        SceneManager.LoadScene("MainMenu");
    }

    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    private void SetVolume(float volumen)
    {
        MusicManager.Instance.SetVolume(volumen);
    }
}
