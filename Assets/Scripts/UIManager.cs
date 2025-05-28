using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crono : MonoBehaviour
{
    public TextMeshProUGUI cronometroText;
    public TextMeshProUGUI nivelText;
    public TextMeshProUGUI vidasText;
    public static Crono instance;

    private float tiempo;
    public float tiempoLimite = 30f; // Tiempo máximo en segundos
    private bool cronometroActivo = true;
    private int nivel = 1;
    private int vida = 100;

    public float TiempoFinal => tiempo;


    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {

        // Detectar nivel desde nombre de escena
        string nombreEscena = SceneManager.GetActiveScene().name;

        if (nombreEscena.StartsWith("Nivel"))
        {
            string numero = nombreEscena.Substring(5); // "Nivel2" -> "2"
            if (int.TryParse(numero, out int nivelDetectado))
            {
                nivel = nivelDetectado;
                PlayerPrefs.SetInt("NivelActual", nivelDetectado);
            }
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        tiempo = 0;
        nivelText.text = $"Nivel {nivel}";
        vidasText.text = $"Vida: {vida}";
    }

    void Update()
    {

        if (!cronometroActivo) return;

        if (tiempo >= tiempoLimite * 0.75f)
        {
            cronometroText.color = Color.red;
        }
        else
        {
            cronometroText.color = Color.white;
        }

        if (tiempo >= tiempoLimite)
        {
            cronometroActivo = false;

            // Guardar que se ha perdido
            PlayerPrefs.SetFloat("TiempoFinal", tiempo);
            PlayerPrefs.SetInt("PuntosUltimoNivel", 0);
            PlayerPrefs.SetInt("Derrota", 1); // marcar que fue derrota

            SceneManager.LoadScene("PantallaDerrota");
            return;
        }

        tiempo += Time.deltaTime;
        int minutos = Mathf.FloorToInt(tiempo / 60);
        int segundos = Mathf.FloorToInt(tiempo % 60);
        int minutosMax = Mathf.FloorToInt(tiempoLimite / 60);
        int segundosMax = Mathf.FloorToInt(tiempoLimite % 60);
        cronometroText.text = $"{minutos:00}:{segundos:00} / {minutosMax:00}:{segundosMax:00}";
    }

    // Método para actualizar vidas más adelante
    public void SetVidas(int nuevasVidas)
    {
        vida = nuevasVidas;
        vidasText.text = $"Vida: {vida}";
    }

    public void PararCronometro()
    {
        cronometroActivo = false;
    }
}
