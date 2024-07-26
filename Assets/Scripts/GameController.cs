using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public GameObject gameOverPanel;

    private float timeRemaining = 60f;
    private bool timerIsRunning = false;

    void Start()
    {
        timerIsRunning = true;
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                GameOver();
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true);
    }
}
