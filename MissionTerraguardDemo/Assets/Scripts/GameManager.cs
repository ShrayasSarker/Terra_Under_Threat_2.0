using UnityEngine;
using UnityEngine.UI;
using TMPro; // Only if using TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton

    public int score = 0;
    public float timeLeft = 60f; // 1 minute

    public TextMeshProUGUI scoreText; // For TMP
    public TextMeshProUGUI timerText; // For TMP
    // If you use normal UI.Text instead of TMP, replace with:
    // public Text scoreText;
    // public Text timerText;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        UpdateScoreUI();
        UpdateTimerUI();
    }

    void Update()
    {
        // Decrease time
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0) timeLeft = 0;

        UpdateTimerUI();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }

    void UpdateTimerUI()
    {
        timerText.text = "Time: " + Mathf.CeilToInt(timeLeft);
    }
}
