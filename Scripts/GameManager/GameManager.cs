using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // ========================================= // 
    [Header("Utility values for game")]

    [Tooltip("Limit of enemies can pass before losing the level")]
    [SerializeField]
    int enemyPassLimit = 5;
    // ========================================= //
    [Header("Text Field References")]

    [Tooltip("Text field for timer at topright")]
    [SerializeField]
    Text timerText;

    [Tooltip("Text field for timer till next wave at topright")]
    [SerializeField]
    Text timerForNextWave;

    [Tooltip("Text field for enemy amount of next wave at topright")]
    [SerializeField]
    Text enemyAmountNextWave;

    [Tooltip("Text field for how many lives player have")]
    [SerializeField]
    Text livesText;

    [Tooltip("Money Text Field")]
    [SerializeField]
    Text moneyText;

    [Tooltip("Wave Passed Count Text")]
    [SerializeField]
    Text wavePassedCountText;

    [Tooltip("Pause Button Text")]
    [SerializeField]
    Text pauseButtonText;
    // ========================================= //

    static int enemiesPassedLine;
    static GameObject gameOverScreen;
    static GameObject pauseMenu;
    static bool isGameOver;
    static bool isGamePaused;
    WaveSpawner waveSpawner;
    float startTime;

    private void Awake()
    {
        gameOverScreen = GameObject.Find("GameOverScreen");
        pauseMenu = GameObject.Find("PauseMenu");
        waveSpawner = FindAnyObjectByType<WaveSpawner>();
        gameOverScreen.SetActive(false);
        pauseMenu.SetActive(false);
    }
    private void Start()
    {
        enemiesPassedLine = 0;
        isGameOver = false;
        isGamePaused = false;
        startTime = Time.deltaTime;
    }

    private void Update()
    {
        TextFieldDisplays();
        if (enemiesPassedLine >= enemyPassLimit)
        {
            GameOver();
        }

        pauseButtonText.text = isGamePaused ? "Resume" : "Pause";
    }

    void TextFieldDisplays()
    {
        timerForNextWave.text = "Next wave in: " + waveSpawner.GetCountdown().ToString("f0");
        livesText.text = (enemyPassLimit - enemiesPassedLine).ToString() + " lives left";
        timerText.text = RealTimer();
        enemyAmountNextWave.text = (waveSpawner.GetEnemyNumber() + 1).ToString() + " enemies will be spawn next wave";
        moneyText.text = "$" + PlayerStats.Money.ToString();
        wavePassedCountText.text = waveSpawner.getWaveCount.ToString() + " waves survived";
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            gameOverScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void Pause()
    {
        if (!isGamePaused)
        {
            isGamePaused = true;
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        else
        {
            isGamePaused = false;
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }

    public static bool GetIsGameOver()
    {
        return isGameOver;
    }

    public static void EnemyPassedLine()
    {
        enemiesPassedLine++;
    }

    public void SpeedUpEnemies()
    {
        EnemyMovement[] allEnemyMovements = FindObjectsOfType<EnemyMovement>();

        foreach (EnemyMovement enemy in allEnemyMovements)
        {
            enemy.SpeedUp();
        }
    }

    string RealTimer()
    {
        float t = Time.time - startTime;
        string minutes = ((int)t / 60).ToString();
        string seconds = t > 9.5f ? (t % 60).ToString("f0") : "0" + (t % 60).ToString("f0");
        return minutes + ":" + seconds;
    }

    public void RestartButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
