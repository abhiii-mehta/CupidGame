using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;

    public TMP_Text scoreText;
    public TMP_Text arrowsText;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateArrows(int arrows)
    {
        arrowsText.text = "Arrows: " + arrows;
    }
}
