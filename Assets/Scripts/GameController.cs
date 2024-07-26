using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public GameObject winPanel;
    public float TimeRemaining = 60f;
    public int ScoreObjective;

    public AudioSource DeathAudio;
    public AudioSource WinAudio;

    private int score;

    private bool timerIsRunning = false;
    private bool isGameOver = false;
    private bool isWin = false;

    void Start()
    {
        timerIsRunning = true;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        scoreText.text = string.Format("{0}/{1}", score, ScoreObjective);

        if (timerIsRunning && !isWin)
        {
            if (TimeRemaining > 0)
            {
                TimeRemaining -= Time.deltaTime;
            }
            else
            {
                TimeRemaining = 0;
                timerIsRunning = false;
                GameOver();
            }
            DisplayTime(TimeRemaining);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void AddScore(int points)
    {
        score += points;
        if(score >= ScoreObjective)
        {
            Win();
        }
    }

    public void SetIsGameOver(bool result)
    {
        isGameOver = result;
    }

    public bool IsWin()
    {
        return isWin;
    }

    public bool IsFinished()
    {
        return (isWin || isGameOver);
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    void Win()
    {
        isWin = true;
        winPanel.SetActive(true);
        WinAudio.Play();
    }

    void GameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);
        DeathAudio.Play();
    }
}
