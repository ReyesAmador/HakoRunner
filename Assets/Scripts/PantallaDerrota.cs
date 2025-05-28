using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaDerrota : MonoBehaviour
{
    public TextMeshProUGUI textoDerrota;


    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        int puntosTotales = PlayerPrefs.GetInt("PuntosTotales", 0);
        textoDerrota.text = $"Has conseguido un total de {puntosTotales} puntos.";
    }

    public void ReintentarNivel()
    {
        PlayerPrefs.DeleteKey("TiempoFinal");
        PlayerPrefs.DeleteKey("PuntosUltimoNivel");
        PlayerPrefs.DeleteKey("Derrota");
        // Recarga el nivel actual
        int nivel = PlayerPrefs.GetInt("NivelActual", 1);
        SceneManager.LoadScene($"Nivel{nivel}");
    }

    public void VolverAlMenu()
    {
        Debug.Log("Volviendo al menú, reseteando puntos");
        PlayerPrefs.DeleteAll(); // borra todos los puntos
        SceneManager.LoadScene("MainMenu");
    }
}
