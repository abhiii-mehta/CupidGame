using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject victoryPanel;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
        SetCursorState(isPaused);
    }


    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        SetCursorState(true);
    }


    public void ShowVictory()
    {
        victoryPanel.SetActive(true);
        Time.timeScale = 0f;
        SetCursorState(true);
    }


    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        SetCursorState(false);
    }

    void SetCursorState(bool isVisible)
    {
        Cursor.visible = isVisible;
        Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
    }

}
