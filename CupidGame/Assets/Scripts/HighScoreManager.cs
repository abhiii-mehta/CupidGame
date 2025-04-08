using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance;

    public string saveKey = "HighScore";
    private int highScore = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScore();
    }

    public void TrySetNewHighScore(int score)
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(saveKey, highScore);
            PlayerPrefs.Save();
            Debug.Log("New High Score: " + highScore);
        }
    }

    public int GetHighScore()
    {
        return highScore;
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt(saveKey, 0);
    }
}
