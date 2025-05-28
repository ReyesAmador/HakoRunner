using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaVictoria : MonoBehaviour
{
    public TextMeshProUGUI textoVictoria;
    public TextMeshProUGUI textoPuntosNivel;
    public TextMeshProUGUI textoPuntosTotales;

    void Start()
    {

        // Mostrar y desbloquear el cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        float tiempoFinal = PlayerPrefs.GetFloat("TiempoFinal", 0f);
        int minutos = Mathf.FloorToInt(tiempoFinal / 60);
        int segundos = Mathf.FloorToInt(tiempoFinal % 60);
        int nivel = PlayerPrefs.GetInt("NivelActual", 1);
        textoVictoria.text = $"¡Nivel {nivel} completado en {minutos:00}:{segundos:00}!";

        // Calcular puntos obtenidos
        int puntosNivel = Mathf.Max(0, 1000 - Mathf.FloorToInt(tiempoFinal * 10));
        PlayerPrefs.SetInt("PuntosUltimoNivel", puntosNivel); // por si quieres mostrarlo
        textoPuntosNivel.text = $"Has conseguido {puntosNivel} puntos!";

        // Sumar al total acumulado
        int puntosTotales = PlayerPrefs.GetInt("PuntosTotales", 0);
        puntosTotales += puntosNivel;
        PlayerPrefs.SetInt("PuntosTotales", puntosTotales);
        textoPuntosTotales.text = $"Tienes {puntosTotales} puntos!";

        Debug.Log($"Puntos ganados: {puntosNivel}, Total: {puntosTotales}");
    }

    public void VolverAlMenu()
    {
        Debug.Log("Volviendo al menú, reseteando puntos");
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MainMenu");
    }

    public void SiguienteNivel()
    {
        // Si vienes de derrota, no puedes avanzar
        if (PlayerPrefs.GetInt("Derrota", 0) == 1)
        {
            Debug.Log("No puedes avanzar, perdiste.");
            return; // o mostrar un mensaje
        }

        int nivelActual = PlayerPrefs.GetInt("NivelActual", 1);
        int siguienteNivel = nivelActual + 1;
        string nombreEscenaSiguiente = $"Nivel{siguienteNivel}";

        // Comprobamos si la escena existe
        if (SceneExists(nombreEscenaSiguiente))
        {
            PlayerPrefs.SetInt("NivelActual", siguienteNivel);
            SceneManager.LoadScene(nombreEscenaSiguiente);
        }
        else
        {
            Debug.Log("Has completado todos los niveles 🎉");
            // SceneManager.LoadScene("PantallaFinal"); // si haces una escena final
        }
        // SceneManager.LoadScene("Nivel2"); // cuando lo crees
    }

    private bool SceneExists(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string name = System.IO.Path.GetFileNameWithoutExtension(path);
            if (name == sceneName)
                return true;
        }
        return false;
    }
}
