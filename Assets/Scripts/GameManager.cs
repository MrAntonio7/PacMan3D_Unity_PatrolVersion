using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton para el Game Manager
    public GameObject[] pointsObject;
    public int pointsCollected;
    public int score; // Puntaje del juego
    public int life; // Vidas del juego
    public bool isGamePaused = false; // Estado de pausa del juego
    public TextMeshProUGUI textVidas;
    public TextMeshProUGUI textPuntos;
    private bool winGame;
    private bool defeatedPlayer;
    public GameObject winPanel;
    public GameObject defeatedPanel;
    public GameObject MenuPanel;
    public GameObject menuCreditos;
    public GameObject timer1;
    public GameObject timer2;
    public float playerVelocity;
    public float ghostVelocity;
    public float timeVulnerabilityGhost;
    public GameObject player;
    public GameObject[] enemys;
    public GameObject[] pointFruits;
    public AudioSource audioMenu;
    //public AudioSource audioPoint;
    public AudioSource audioFruit;
    public AudioSource audioWalk;
    public AudioSource audioDead;
    public AudioSource audioCanEat;
    public GameObject audioEatGhost;
    void Awake()
    {   

        // Asegurarse de que solo haya una instancia del Game Manager
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // No destruir el objeto al cambiar de escena
        }
        //else
        //{
        //    Destroy(gameObject); // Destruir si ya existe una instancia
        //}
    }
    private void Start()
    {
        PauseGame();
        defeatedPlayer = false;
        winGame = false;
        score = 0;
        life = 3;
        pointsCollected = 0;
        pointsObject = GameObject.FindGameObjectsWithTag("Point");
    }
    private void Update()
    {
        textPuntos.text = score.ToString();
        textVidas.text = life.ToString();

        //Partida ganada
        if (pointsCollected >= 254 && !winGame)
        {
            audioWalk.Stop();
            winGame = true;
            winPanel.SetActive(true);
            timer1.GetComponent<TimerController>().StopStopwatch();
            timer2.GetComponent<TimerController>().StopStopwatch();
            PauseGame();
            Debug.Log("Has ganado la partida");
        }

        //Partida perdida
        if(life <=0 && !defeatedPlayer)
        {
            audioWalk.Stop();
            
            defeatedPlayer = true;
            defeatedPanel.SetActive(true);
            timer1.GetComponent<TimerController>().StopStopwatch();
            timer2.GetComponent<TimerController>().StopStopwatch();
            PauseGame();
            Debug.Log("Has perdido la partida");
        }
    }

    // Método para aumentar el puntaje
    public void AddScore(int points)
    {
        if (points == 1)
        {
            GetComponent<OverlappingAudio>().PlaySound();
        }
        if (points == 5)
        {
            audioFruit.Play();
        }
        score += points;
    }
    public void AddPointCollect()
    {
        pointsCollected++;
        //Debug.Log(pointsCollected);
    }

    // Método para obtener el puntaje actual
    public int GetScore()
    {
        return score;
    }

    // Método para restablecer el puntaje
    public void ResetScore()
    {
        score = 0;
    }

    // Método para pausar el juego
    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0; // Detener el tiempo en el juego
    }

    // Método para reanudar el juego
    public void ResumeGame()
    {
        audioWalk.Play();
        isGamePaused = false;
        Time.timeScale = 1; // Restablecer el tiempo en el juego
    }

    // Método para cambiar de escena
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // Cargar una nueva escena
    }

    // Método para reiniciar el nivel actual
    public void RestartLevel()
    {
        ResumeGame();
        timer1.GetComponent<TimerController>().isRunning = true;
        timer2.GetComponent<TimerController>().isRunning = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reiniciar la escena actual
    }

    // Método para salir del juego
    public void QuitGame()
    {
        Application.Quit(); // Salir del juego
    }

    public void Facil()
    {
        MenuPanel.SetActive(false);
        timer1.GetComponent<TimerController>().isRunning = true;
        timer2.GetComponent<TimerController>().isRunning = true;
        playerVelocity = 5f;
        ghostVelocity = 4f;
        timeVulnerabilityGhost = 9f;
        life = 3;
        SetDificultad();
        ResumeGame() ;
    }
    public void Normal()
    {
        MenuPanel.SetActive(false);
        timer1.GetComponent<TimerController>().isRunning = true;
        timer2.GetComponent<TimerController>().isRunning = true;
        playerVelocity = 4f;
        ghostVelocity = 4f;
        timeVulnerabilityGhost = 6f;
        life = 2;
        SetDificultad();
        ResumeGame();
    }
    public void Dificil()
    {
        MenuPanel.SetActive(false);
        timer1.GetComponent<TimerController>().isRunning = true;
        timer2.GetComponent<TimerController>().isRunning = true;
        playerVelocity = 3.5f;
        ghostVelocity = 5f;
        timeVulnerabilityGhost = 4f;
        life = 1;
        SetDificultad();
        ResumeGame();
    }
    public void SetDificultad()
    {
        player.GetComponent<NavMeshAgent>().speed = playerVelocity;
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].GetComponent<NavMeshAgent>().speed = ghostVelocity;
        }
        for (int j = 0; j< pointFruits.Length; j++)
        {
            pointFruits[j].GetComponent<PointEatGhost>().timeVulnarability = timeVulnerabilityGhost;
            Debug.Log(pointFruits[j].name);
        }
        audioMenu.Stop();
    }
    public void Creditos()
    {
        MenuPanel.SetActive(false);
        menuCreditos.SetActive(true);
    }
    public void ActivarMenu()
    {
        MenuPanel.SetActive(true);
        menuCreditos.SetActive(false);
    }
}